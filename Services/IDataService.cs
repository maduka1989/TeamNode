using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NodeCMBAPI.Models;

namespace NodeCMBAPI.Services
{
    public interface IDataService
    {
        #region Food Portion
        List<Food_Portion> GetFoodPortion();
        Food_Portion GetFoodPortionById(int id);
        string AddFoodPortion(Food_Portion foodPortion);
        string UpdateFoodPortion(Food_Portion foodPortion);

        #endregion

    }

    public interface IUserService
    {
        #region User
        List<User> GetUsers();
        User Authenticate(string username, string password);
        User GetById(int id);
        string UpdateUser(User user);
        string Register(User user);
        string ResetPassword(User user);

        #endregion
    }

    public interface IHotelService
    {
        List<Hotel> GetHotels();
        Hotel GetById(int id);
        string AddHotel(Hotel hotel);
        string UpdateHotel(Hotel hotel);
    }

    public interface IRestaurentService
    {
        List<Restaurent> GetRestaurent();
        Restaurent GetById(int id);
        string AddRestaurent(Restaurent restaurent);
        string UpdateRestaurent(Restaurent restaurent);
    }

    public interface INodeCustomersService
    {
        List<Node_Customers> GetNodeCustomers();
        Node_Customers GetById(int id);
        string AddNodeCustomers(Node_Customers nodeCustomer);
        string UpdateNodeCustomers(Node_Customers nodeCustomers);
    }

    public interface IFoodService
    {
        List<Food> GetFood();
        Food GetById(int id);
        string AddFood(Food food);
        string UpdateFood(Food food);
    }

    public interface IQuantityTypesService
    {
        List<QuantityTypes> GetQuantityTypes();
        QuantityTypes GetById(int id);
        string AddQuantityTypes(QuantityTypes qt);
        string UpdateQuantityTypes(QuantityTypes qt);
    }

    public interface IRawMaterialsService
    {
        List<Raw_Materials> GetRawMaterials();
        Raw_Materials GetById(int id);
        string AddRawMaterials(Raw_Materials rm);
        string UpdateRawMaterials(Raw_Materials rm);
    }

    public interface IAddtionitemsmenuService
    {
        List<Addtion_items_menu> GetAddtionitemsmenu();
        Addtion_items_menu GetById(int id);
        string AddAddtionitemsmenu(Addtion_items_menu aim);
        string UpdateAddtionitemsmenu(Addtion_items_menu aim);
    }

    public interface IHappyHourService
    {
        List<Happy_Hour> GetHappyHour();
        Happy_Hour GetById(int id);
        string AddHappyHour(Happy_Hour hh);
        string UpdateHappyHour(Happy_Hour hh);
    }

    public interface IMenuService
    {
        List<Menu> GetMenu();
        Menu GetById(int id);
        string AddMenu(Menu menu);
        string UpdateMenu(Menu menu);
    }

    public interface IPriceService
    {
        List<Price> GetPrice();
        Price GetById(int id);
        string AddPrice(Price price);
        string UpdatePrice(Price price);
    }

    public interface IMenuItemsService
    {
        List<Menu_Items> GetMenuItems();
        Menu_Items GetById(int id);
        string AddMenuItems(Menu_Items mi);
        string UpdateMenuItems(Menu_Items mi);
    }

    public interface IIngredientQtyPerDishService
    {
        List<Ingredient_Qty_Per_Dish> GetIngredientQtyPerDish();
        Ingredient_Qty_Per_Dish GetById(int id);
        string AddIngredientQtyPerDish(Ingredient_Qty_Per_Dish iqpd);
        string UpdateIngredientQtyPerDish(Ingredient_Qty_Per_Dish iqpd);
    }

    public interface ITablesService
    {
        List<Tables> GetTables();
        Tables GetById(int id);
        string AddTables(Tables table);
        string UpdateTables(Tables table);
    }

    public interface ITaxService
    {
        List<Tax> GetTax();
        Tax GetById(int id);
        string AddTax(Tax tax);
        string UpdateTax(Tax tax);
    }
    public interface IUserRolesService
    {
        List<UserRoles> GetUserRoles();
        UserRoles GetById(int id);
        string AddUserRoles(UserRoles ur);
        string UpdateUserRoles(UserRoles ur);
    }

    public interface ISubMenuMasterService
    {
        List<SubMenu_Master> GetSubMenuMaster();
        SubMenu_Master GetById(int id);
        string AddSubMenuMaster(SubMenu_Master smm);
        string UpdateSubMenuMaster(SubMenu_Master smm);
    }

    public interface IOrderTypeService
    {
        List<OrderType> GetOrderType();
        OrderType GetById(int id);
        string AddOrderType(OrderType ot);
        string UpdateOrderType(OrderType ot);
    }

    public interface IOrderService
    {
        List<Order> GetOrder();
        Order GetById(int id);
        string AddOrder(Order ot);
        string UpdateOrder(Order ot);
    }

    public interface IOrderedItemsService
    {
        List<OrderedItems> GetOrderedItems();
        OrderedItems GetById(int id);
        string AddOrderedItems(OrderedItems ot);
        string UpdateOrderedItems(OrderedItems ot);
    }
}
