

namespace CodeTimer {

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
            return (string.IsNullOrEmpty(this.Name)) ?
                    $"{this.Ticks} - {this.Name}" :
                    $"{this.Ticks}";
        }
    }
}