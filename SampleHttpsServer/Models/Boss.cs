namespace SampleHttpsServer
{
    using JsGridLib.Contracts;

    public class Boss : IJsGridEntity
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }
}