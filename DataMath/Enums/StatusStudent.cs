using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMath.Enums
{
    public enum StatusStudent
    {
        Active = 1,       // Активный студент (учится в группе)
        Graduated = 2,    // Выпускник (завершил обучение в группе)
        DroppedOut = 3,   // Отчислен (перестал учиться)
        Transferred = 4,  // Переведён в другую группу
        Suspended = 5     // Временно отстранён
    }
}
