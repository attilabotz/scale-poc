using System;
using System.Data.Common;
using Paramore.Brighter;

namespace WebAPI.DataAccess
{
    public class EntityFwTransactionConnectionProvider : IAmABoxTransactionConnectionProvider
    {
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