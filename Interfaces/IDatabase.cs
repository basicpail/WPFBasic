using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFBasic.Interfaces
{
    public interface IDatabase<T>
    {
        //테이블에 대한 모든 데이터 조회
        //테이블이 여러개 있을 경우 이 클래스를 범용적으로 쓰기위해 T 제네릭으로 사용
        //이 인터페이스를 서비스에서 상속받아 사용하도록 구성
        //List<T>? -> 반환 타입은 List<T> 인데, null 을 반환할 수도 있다라고 알려주는것
        List<T>? Get();

        //테이블에 대해 특정 ID에 해당하는 데이터 조회
        T? GetDetail(int? id);

        //테이블에 특정 DATA INSERT
        void Create(T entity);

        //테이블에 특정 DATA UPDATE
        void Update(T entity);

        //테이블에 특정 DATA DELETE
        void Delete(int? id);
    }
}
