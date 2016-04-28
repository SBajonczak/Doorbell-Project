using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace Doorbell
{
    public class DoorbellViewModel
    {
        static DeviceClient deviceClient;
        static string iotHubUri = "DataFleet.azure-devices.net";
        static string deviceKey = "5X4v034PULDbjiFug83oUzm0PlAYq3V0fe5niGBnhq8=";


        /// <summary>
        /// Doorbell switch
        /// </summary>
        public static int ButtonPinNumber = 21;

        /// <summary>
        /// The Signall will be redirected to this port.
        /// </summary>
        public static int OutputPinNumber = 5;

        GpioPin buttonPin;
        GpioPin ouputPin;

        public DoorbellViewModel()
        {
            this.InitializeGpio();
            this.InitializeAzureIOTHub();
            this.TransferRingToAzure();
        }

        private void InitializeAzureIOTHub()
        {
            deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey("myFirstDevice", deviceKey));

        }

        private void InitializeGpio()
        {
            //Create a default GPIO controller
            GpioController gpioController = GpioController.GetDefault();
            if (gpioController != null) {
                //Use the controller to open the gpio pin of given number
                buttonPin = gpioController.OpenPin(ButtonPinNumber);
                //Debounce the pin to prevent unwanted button pressed events
                buttonPin.DebounceTimeout = new TimeSpan(1000);
                //Set the pin for input
                buttonPin.SetDriveMode(GpioPinDriveMode.Input);
                //Set a function callback in the event of a value change
                buttonPin.ValueChanged += buttonPin_ValueChanged;

                ouputPin = gpioController.OpenPin(OutputPinNumber);
                ouputPin.SetDriveMode(GpioPinDriveMode.Output);
            }
         }

        //This method will be called everytime there is a change in the GPIO pin value
        private async void buttonPin_ValueChanged(object sender, GpioPinValueChangedEventArgs e)
        {
            Debug.WriteLine("Klingel");
            //Only read the sensor value when the button is released
            if (e.Edge == GpioPinEdge.RisingEdge)
            {

                this.TransferRingToBell();
                this.TransferRingToAzure();    
            }
        }

        private async void TransferRingToBell()
        {
            if (ouputPin != null)
            {

                ouputPin.Write(GpioPinValue.High);
                await Task.Delay(1000);
                ouputPin.Write(GpioPinValue.Low);
            }
        }


        private async void TransferRingToAzure()
        {
            var data = new DoorbellData("Haustür");
            var messageString = JsonConvert.SerializeObject(data);
            var message = new Message(Encoding.ASCII.GetBytes(messageString));

            await deviceClient.SendEventAsync(message);
            
        }


    }

}
