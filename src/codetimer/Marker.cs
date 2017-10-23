

namespace CodeTimer {

    /// <summary>
    /// Marker is a data collection item for each point collected during a timing operation.
    /// </summary>
    /// <example>
    /// This example shows 2 Markers named Operation 1 and Operation 2 collected at runtime:
    /// 
    /// var timer = new CodeTimer("MyCodeTimer");
    /// 
    /// timer.Mark("Operation 1");
    /// // ...
    /// timer.Mark("Operation 2");
    /// </example>
    public class Marker {
        private readonly string name;
        private readonly long ticks;

        public Marker(long ticks, string name = "") {
            this.name = name;
            this.ticks = ticks;
        }

        public string Name => name;

        public long Ticks => ticks;

        public override string ToString() {

            var name = this.Name ?? "";

            return (string.IsNullOrEmpty(name.Trim())) ?
                    $"{this.Ticks}" :
                    $"{this.Ticks} - {this.Name}";
        }
    }
}