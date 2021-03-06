﻿using System;
using System.Collections.Generic;
using NetCoreModel;
using NetCoreRepository;

namespace NetCoreService
{
    public class UserService: IUserService
    {
        protected IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public bool AddUser(UserTwo model)
        {
            bool flag = false;
            try
            {
                var dbtran = _userRepository.DbTransaction;
                _userRepository.AddModel(model, dbtran);
                //dbtran.Rollback();
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }

        public bool AddUsers(IEnumerable<UserTwo> models)
        {
            throw new NotImplementedException();
        }

        public long GetUsersCount()
        {
            try
            {
                string sql = "select count(*) from test";
                return _userRepository.GetCount(sql);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
