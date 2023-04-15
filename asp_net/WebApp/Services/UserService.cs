using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApp.Contexts;
using WebApp.Models.Entities;
using WebApp.ViewModels;

//namespace WebApp.Services
//{
//    public class UserService
//    {
//        private readonly DataContext _dataContext;

//        public UserService(DataContext dataContext)
//        {
//            _dataContext = dataContext;
//        }

//        public async Task<bool> CheckIfUserExists(Expression<Func<UserEntity, bool>> predicate)
//        {
//            if(await _dataContext.Users.AnyAsync(predicate)) {
//                return true;}
//            return false;
//        }
//        public async Task<UserEntity> GetUserAsync(Expression<Func<UserEntity, bool>> predicate)
//        {
//            var userEntity = await _dataContext.Users.FirstOrDefaultAsync(predicate);
//            return userEntity!;
//        }



//        public async Task<bool> RegisterAsync(RegisterViewModel registerViewModel)
//        {
//            try
//            {
//                UserEntity userEntity = registerViewModel;
//                ProfileEntity profileEntity = registerViewModel;

//                _dataContext.Users.Add(userEntity);
//                await _dataContext.SaveChangesAsync();

//                profileEntity.UserId = userEntity.Id;
//                _dataContext.Profiles.Add(profileEntity);
//                await _dataContext.SaveChangesAsync();

//                return true;
//                }

//            catch
//            {
//                return false;
//            }
//        }
//        public async Task<bool> LogInAsync(LogInViewModel logInViewModel)
//        {
//            var userEntity = await GetUserAsync(x => x.Email == logInViewModel.Email);
//            if (userEntity != null)
//            {
//                return userEntity.VerifySecurePassword(logInViewModel.Password);
//            }
//            return false;
//        }
//    }
//}
