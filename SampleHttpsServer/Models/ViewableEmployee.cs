﻿namespace SampleHttpsServer
{
    using JsGridLib.Contracts;

    public class ViewableEmployee : IJsGridEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Contact { get; set; }
        public string Baa { get; set; }
    }
}