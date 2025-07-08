using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFBasic.Interfaces;
using WPFBasic.Models;

//범용적인 기능들은 Interface화 시켜서 상속 받아 사용하고
//아니면 다른 기능이 있는 클래스를 상속 받는게 아니라, 생성자 주입을 통해 초기화 시켜서 사용한다.

namespace WPFBasic.Services
{
    class GangnamguPopulationService : IDatabase<GangnamguPopulation>
    {
        private readonly WpfProjectDatabaseContext? _wpfProjectDatabaseContext;

        //DB Context를 주입받는 생성자
        public GangnamguPopulationService(WpfProjectDatabaseContext wpfProjectDatabaseContext)
        {
            this._wpfProjectDatabaseContext = wpfProjectDatabaseContext;
        }
        public void Create(GangnamguPopulation entity)
        {
            //Context에서 각각의 테이블이 DbSet로 클래스화가 되어서 이걸 가져다 쓸 수 있다.
            this._wpfProjectDatabaseContext?.GangnamguPopulations.Add(entity);
            this._wpfProjectDatabaseContext?.SaveChanges();
        }

        public void Delete(int? id)
        {
            //매개변수로 받은 id가 존재하는지 확인
            var validData = this._wpfProjectDatabaseContext?.GangnamguPopulations.FirstOrDefault(c => c.Id == id);

            if (validData != null)
            {
                this._wpfProjectDatabaseContext?.GangnamguPopulations.Remove(validData);
                this._wpfProjectDatabaseContext?.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("해당 ID에 대한 데이터가 존재하지 않습니다.");
            }
        }

        public List<GangnamguPopulation>? Get()
        {
        //GangnamguPopulation 테이블에 있는 모든 데이터를 조회
            return this._wpfProjectDatabaseContext?.GangnamguPopulations.ToList();
        }

        public GangnamguPopulation? GetDetail(int? id)
        {
            var validData = this._wpfProjectDatabaseContext?.GangnamguPopulations.FirstOrDefault(c => c.Id == id);

            if (validData != null)
            {
                return validData;
            }
            else
            {
                throw new InvalidOperationException("해당 ID에 대한 데이터가 존재하지 않습니다.");
            }
        }

        public void Update(GangnamguPopulation entity)
        {
            var validData = this._wpfProjectDatabaseContext?.GangnamguPopulations.FirstOrDefault(c => c.Id == entity.Id);

            if (validData != null)
            {
                this._wpfProjectDatabaseContext?.GangnamguPopulations.Update(entity);
                this._wpfProjectDatabaseContext?.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("해당 ID에 대한 데이터가 존재하지 않습니다.");
            }
        }
    }
}
