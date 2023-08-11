# NosiYa
Inspired by https://www.facebook.com/nosiyapodnaem

/Not Yet Seeded with actual data/

__How to start the Project:__ 

__1. Add Connection string to user secrets /__
```raw
  {
  "ConnectionStrings": {
    "DefaultConnection": "Server=<host>;Database=NosiYa;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "SendGridApiKey": "SG....."
  }
}
```
__2. Set NosiYa.Web as starting project__
__3. In package management console run the :__
```raw
   update-database InitialDbCreation
```

__4. Go to Management Studio > AspNetUsers > Edit top 200 > Select then righclick the top left cornef of the table and paste :__
   /In case of issuess copy the content from the raw format of this document/

```raw
7c34fb52-0fdb-4cd7-027f-08db822aa1b7	admin@nosiya.com	ADMIN@NOSIYA.COM	admin@nosiya.com	ADMIN@NOSIYA.COM	False	AQAAAAEAACcQAAAAEN9b+fKCBLZMRaLXti2gCl4HXauoim1LMj9GLe/l06/xbA2jwiaeLJg/nhxQxJLsYA==	PRW3JMATEP23MBZLF5JXXR6UPKVG7VF6	64648ab8-12b8-4ce8-8a13-ac490c86b20a	1122334455	True	False	NULL	True	0
2f29d591-89ef-45b2-89a9-08db83ceb60e	user@nosiya.com	USER@NOSIYA.COM	user@nosiya.com	USER@NOSIYA.COM	False	AQAAAAEAACcQAAAAEHeFHkR+kw6eRzy5GRhb9FFP3Dq7iLpytH1WZGUWor6hw/G14jQpgeeJj0rg0bh/DA==	M7BXJDCJKEMCFXOFVEQIHK7HLLDBK4YI	9f442b6c-af34-4441-b833-73c88421b68d	NULL	False	False	NULL	True	0
```

![image](https://github.com/TabakovG/NosiYa/assets/25383066/f92a5abc-d188-4ae8-9577-67b2a7d6b3e7)

__5.  Go to Management Studio > Carts > Edit top 200 and paste :__
   
```raw
1	7c34fb52-0fdb-4cd7-027f-08db822aa1b7
2	2f29d591-89ef-45b2-89a9-08db83ceb60e
```

![image](https://github.com/TabakovG/NosiYa/assets/25383066/18092d68-6329-4a3c-ba56-8b7a1b937fe9)

/The goal is to create the following 2 users and related carts :

        public const string AdminId = "7C34FB52-0FDB-4CD7-027F-08DB822AA1B7";
        public const string UserId = "2F29D591-89EF-45B2-89A9-08DB83CEB60E";

        public const string AdminRoleName = "Admin";
        public const string DevAdminEmail = "admin@nosiya.com";

        public const string UserRoleName = "User";
        public const string DevUserEmail = "user@nosiya.com";
/

__6. Go to Visual Studio > Package management console and run:__

```raw
  update-database
```
  accountspass- 123123
   
#fullCalendar #sendGrid #ajax #lightBox #imageSharp 
