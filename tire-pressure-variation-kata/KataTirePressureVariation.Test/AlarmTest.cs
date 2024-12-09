using NUnit.Framework;

namespace KataTirePressureVariation.Test
{
    public class AlarmTest
    {
        private const double LowestSafePressure = 17;
        private const double HighestSafePressure = 21;
        private const double TooLowPressure = LowestSafePressure - 1;
        private const double TooHighPressure = HighestSafePressure + 1;
        private AlarmForTesting? _alarm;
        

        [Test]
        public void Alarm_Activates_When_Sampled_Pressure_Is_Too_Low()
        {
            _alarm = AnAlarmThatSamples(TooLowPressure);

            _alarm.Check();

            ExpectThatDisplayedMessagesWere("Alarm activated!");
        }

        [Test]
        [TestCase(LowestSafePressure)]
        [TestCase(HighestSafePressure)]
        public void Alarm_Does_Not_Activate_When_Sampled_Pressure_Is_Safe(double sampledPressure)
        {
            _alarm = AnAlarmThatSamples(sampledPressure);

            _alarm.Check();

            ExpectThatThereWereNoDisplayedMessages();
        }

        [Test]
        public void Alarm_Activates_When_Sampled_Pressure_Is_Too_High()
        {
            _alarm = AnAlarmThatSamples(TooHighPressure);

            _alarm.Check();

            ExpectThatDisplayedMessagesWere("Alarm activated!");
        }
        
        [Test]
        public void Activated_Alarm_Deactivates_When_Sampled_Pressure_Is_Safe()
        {
            _alarm = AnAlarmThatSamples(TooHighPressure, LowestSafePressure);
            _alarm.Check();
            
            _alarm.Check();

            ExpectThatDisplayedMessagesWere("Alarm activated!", "Alarm deactivated!");
        }
        
        private static AlarmForTesting AnAlarmThatSamples(params double[] pressureValue)
        {
            return new AlarmForTesting(pressureValue);
        }
        
        private void ExpectThatDisplayedMessagesWere(params string[] messages)
        {
            Assert.That(_alarm!.DisplayedMessages.Count,Is.EqualTo(messages.Length));
            for (var i = 0; i < messages.Length; i++)
            {
                Assert.That(_alarm.DisplayedMessages[i],Is.EqualTo(messages[i]));
            }
        }
      
        private void ExpectThatThereWereNoDisplayedMessages()
        {
            ExpectThatDisplayedMessagesWere();
        }
    }

    internal class AlarmForTesting : Alarm
    {
        private readonly List<double> _sampledValues;
        private readonly List<string> _displayedMessages;
        private int _numCallsToSampleValue;

        public AlarmForTesting(params double[] pressureValues)
        {
            _sampledValues = new List<double>(pressureValues);
            _numCallsToSampleValue = 0;
            _displayedMessages = new List<string>();
        }

        public List<string> DisplayedMessages => _displayedMessages;

        protected override double SampleValue()
        {
            var sampledValue = _sampledValues[_numCallsToSampleValue];
            _numCallsToSampleValue++;
            return sampledValue;
        }

        protected override void Display(string message)
        {
            _displayedMessages.Add(message);
        }
    }
}