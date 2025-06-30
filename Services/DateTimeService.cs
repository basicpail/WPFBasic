using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFBasic.Interfaces;

namespace WPFBasic.Services
{
    internal class DateTimeService : IDateTime
    {
        //IDateTime 인터페이스를 상속 받았지만 실질적으로 구현해주지 않은 GetCurrentTime 메소드를 여기서 구현해준다.
        public DateTime? GetCurrentTime()
        {
            return DateTime.Now; // 현재 시간을 반환한다.
        }
    }
}
