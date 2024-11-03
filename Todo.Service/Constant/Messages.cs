using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Service.Constant
{
    public static class Messages
    {
        public const string TodoAddedMessage = "todo Eklendi";
        public const string TodoUpdatedMessage = "todo Güncellendi";
        public const string TodoDeletedMessage = "todo Silindi.";
        public static string TodoIsNotPresentMessage(Guid id)
        {
            return $"İlgili id ye göre todo bulunamadı : {id}";
        }
    }
}
