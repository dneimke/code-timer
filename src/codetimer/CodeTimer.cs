using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace CodeTimer {

    public class CodeTimer : ICodeTimer {

        private readonly string name;
        private readonly ILogger logger;

        public CodeTimer(string name)
        {
            this.name = name;
        }

        public CodeTimer(string name, ILogger logger)
        {
            this.name = name;
            this.logger = logger;
        }

        public long ExpectedMilliseconds { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public void Complete()
        {
            throw new System.NotImplementedException();
        }

        public long GetElapsedMilliseconds()
        {
            throw new System.NotImplementedException();
        }

        public string GetFormattedResult()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Segment> GetSegments()
        {
            throw new System.NotImplementedException();
        }

        public void Mark()
        {
            throw new System.NotImplementedException();
        }

        public void Mark(string segmentName)
        {
            throw new System.NotImplementedException();
        }

        public bool Success()
        {
            throw new System.NotImplementedException();
        }
    }
}