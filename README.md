# CoronaSystem
How of Use:
in the home screen there is 3 things:
1. kink to all the members
2. link to the statictis graph
3. pie diagram who represent the amount of people who take the vaccination and not.


   ![image](https://github.com/TamarGefner/CoronaSystem/assets/116760923/ac0d8099-0cb1-4d81-99eb-0aac576ebc0a)



The members Page:
is a list of all the members and each member you can edit, delete and view his personal data
and add a new members.

![image](https://github.com/TamarGefner/CoronaSystem/assets/116760923/15a25e97-b507-4d57-bf8b-7d61d498156d)


The add member page:

![image](https://github.com/TamarGefner/CoronaSystem/assets/116760923/72225d73-b658-412b-85a4-415ded207757)

the personal data page:
from this page you can go to edit the member details and see his history od vaccination and his status health

![image](https://github.com/TamarGefner/CoronaSystem/assets/116760923/3bf718e7-8e91-4c80-90c8-758ddbb25d03)


my vaccination page:
you can vaccination but until 4 vaccinations, and edit and delete any vaccination


![image](https://github.com/TamarGefner/CoronaSystem/assets/116760923/48a2f016-d634-4140-9dcf-fc751319c1bc)


health status page:
if the member was not sick yet pressing this link will open a page that you can update this member is sich

![image](https://github.com/TamarGefner/CoronaSystem/assets/116760923/b74ed326-996a-422a-87f6-152970a90ab3)


if the member was already sick then will be open a pgae that represents the data and an option to edit it or delete in case of mistake


![image](https://github.com/TamarGefner/CoronaSystem/assets/116760923/92990da4-39ee-42e5-bf27-a8766e0673e8)


Bobus : The statictick page:
its a graph who show how many active member were every day in a mounth. the default is the current month and year but there is option to change it

![image](https://github.com/TamarGefner/CoronaSystem/assets/116760923/f17dfd00-bdfa-4636-a83d-dfc078138d83)


anouther bonus: show how many members did not get a vaccination:
i did a pie diagram who represent it on the homepage. when you put your mouse on each part in the diagram it show the amount of people who got the vaccination and no.


![image](https://github.com/TamarGefner/CoronaSystem/assets/116760923/a1cff04b-bd2d-4571-b58e-6052313c12ae)


external dependencies:

add migration:
1. From the Tools menu, select NuGet Package Manager > Package Manager Console .
2. write the following commands:
   - Add-Migration Member
   - Update-Database
   - Add-Migration Vaccination
   - Update-Database
   - Add-Migration SickMember
   - Update-Database


