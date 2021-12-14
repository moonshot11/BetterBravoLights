﻿using BravoLights.Common;
using NLog;

namespace BravoLights
{
    public class GlobalLightController
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IUsbLogic usbLogic;

        public GlobalLightController(IUsbLogic usbLogic)
        {
            this.usbLogic = usbLogic;
        }

        private bool simulatorInMainMenu = false;
        public bool SimulatorInMainMenu { get => simulatorInMainMenu; set { simulatorInMainMenu = value; Check(); } }

        private bool readingConfiguration = false;
        public bool ReadingConfiguration { get => readingConfiguration; set { readingConfiguration = value; Check(); } }

        private bool applicationExiting;
        public bool ApplicationExiting { get => applicationExiting; set { applicationExiting = value; Check(); } }

        private bool simulatorConnected;
        public bool SimulatorConnected { get => simulatorConnected; set { simulatorConnected = value; Check(); } }

        private void Check()
        {
            logger.Debug("SimulatorInMainMenu={0}, ReadingConfiguration={1}, ApplicationExiting={2}, SimulatorConnected={3}",
                SimulatorInMainMenu, ReadingConfiguration, ApplicationExiting, SimulatorConnected);

            var lightsShouldBeOn = !SimulatorInMainMenu && !ReadingConfiguration && !ApplicationExiting && SimulatorConnected;
            usbLogic.LightsEnabled = lightsShouldBeOn;
        }
    }
}
