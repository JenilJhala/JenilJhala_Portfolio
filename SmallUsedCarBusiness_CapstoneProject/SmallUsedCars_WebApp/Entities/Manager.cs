namespace SmallUsedCars_WebApp.Entities
{
    public class Manager : Employee
    {
        // Manager는 Employee를 상속하므로, Id(string)를 자동으로 가짐.
        // Employee는 IdentityUser에서 제공하는 Id(기본적으로 string)를 가짐.
    }
}
