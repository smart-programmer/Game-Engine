using System;
using System.Diagnostics;


namespace TestEngine
{
    class World
    {
        public float LastFrameStartTime { set; get; }
        public float deltaTimeSec { set; get; }

        public World()
        {
            LastFrameStartTime = getCurrentTime();
        }

        public void calculateDeltaTime()
        {
            float currentTime = getCurrentTime();
            deltaTimeSec = (currentTime - LastFrameStartTime) / 1000;
            LastFrameStartTime = currentTime;
        }

        public float getCurrentTime()
        {
            return Stopwatch.GetTimestamp() * 1000 / Stopwatch.Frequency ;
        }
    }
}
