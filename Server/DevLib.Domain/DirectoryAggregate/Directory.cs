﻿using DevLib.Domain.PostAggregate;

namespace DevLib.Domain.DirectoryAggregate
{
    public class DLDirectory
    {
        public Guid DirectoryId { get; set; }
        public string DirectoryName { get; set; }
    }
}
