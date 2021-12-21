namespace AppKi.Tracing
{
    public class TracingSettings
    {
        public bool Enabled { get; set; } = true;
        public string ServiceName { get; set; } = "SomeService";
        public JaegerSettings Jaeger { get; set; } = new();
    }

    public class JaegerSettings
    {
        public string Host { get; set; } = "localhost";
        public int Port { get; set; } = 6831;
    }
}
