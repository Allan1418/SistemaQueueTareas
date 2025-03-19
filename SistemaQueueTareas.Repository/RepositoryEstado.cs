﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaQueueTareas.Data;

namespace SistemaQueueTareas.Repository
{
    public interface IRepositoryEstado : IRepositoryBase<Estado>
    {
    }
    public class RepositoryEstado : RepositoryBase<Estado>, IRepositoryEstado
    {
        public RepositoryEstado() : base()
        {
        }
    }
}
