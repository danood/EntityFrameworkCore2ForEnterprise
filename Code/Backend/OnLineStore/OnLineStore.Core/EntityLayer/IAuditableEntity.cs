﻿using System;

namespace OnLineStore.Core.EntityLayer
{
    public interface IAuditableEntity : IEntity
    {
        string  CreationUser { get; set; }

        DateTime? CreationDateTime { get; set; }

        string  LastUpdateUser { get; set; }

        DateTime? LastUpdateDateTime { get; set; }
    }
}
