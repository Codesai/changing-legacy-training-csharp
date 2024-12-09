using System;

namespace KataTirePressureVariation
{
    public class Alarm
    {
        private const double LowPressureThreshold = 17;
        private const double HighPressureThreshold = 21;

        private readonly Sensor _sensor = new Sensor();
        private bool _alarmOn = false;

        public void Check()
        {
            var psiPressureValue = SampleValue();

            if (psiPressureValue < LowPressureThreshold || HighPressureThreshold < psiPressureValue)
            {
                if (!IsAlarmOn())
                {
                    _alarmOn = true;
                    Display("Alarm activated!");
                }
            }
            else
            {
                if (IsAlarmOn())
                {
                    _alarmOn = false;
                    Display("Alarm deactivated!");
                }
            }
        }

        protected virtual double SampleValue()
        {
            return _sensor.PopNextPressurePsiValue();
        }

        protected virtual void Display(string message)
        {
            Console.WriteLine(message);
        }

        private bool IsAlarmOn()
        {
            return _alarmOn;
        }
    }
}