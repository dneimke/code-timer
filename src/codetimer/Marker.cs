

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

            var name = this.Name ?? "";

            return (string.IsNullOrEmpty(name.Trim())) ?
                    $"{this.Ticks}" :
                    $"{this.Ticks} - {this.Name}";
        }
    }
}