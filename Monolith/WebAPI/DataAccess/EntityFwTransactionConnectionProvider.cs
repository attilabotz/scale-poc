using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Paramore.Brighter;

namespace WebAPI.DataAccess
{
    public class EntityFwTransactionConnectionProvider : IAmABoxTransactionConnectionProvider
    {
        private string Name = Ulid.NewUlid().ToString();
        
        public readonly NoteContext Context;

        public EntityFwTransactionConnectionProvider(NoteContext context)
        {
            Context = context;
        }

        public void Open()
        {
            Context.Database.BeginTransaction();
        }

        public void Close()
        {
            Context.Database.CurrentTransaction.Commit();
        }

        public void Rollback()
        {
            Context.Database.CurrentTransaction.Rollback();
        }
    }
}