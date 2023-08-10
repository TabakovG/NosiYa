# NosiYa
Inspired by https://www.facebook.com/nosiyapodnaem

How to start the Project:
1. Add Connection string to user secrets / 
  {
  "ConnectionStrings": {
    "DefaultConnection": "Server=<host>;Database=NosiYa;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "SendGridApiKey": "SG....."
  }
}

2. Set NosiYa.Web as starting project
3. In package management console run the :
   
   update-database InitialDbCreation

4. Go to Management Studio > AspNetUsers > Edit top 200 and paste :

7c34fb52-0fdb-4cd7-027f-08db822aa1b7	admin@nosiya.com	ADMIN@NOSIYA.COM	admin@nosiya.com	ADMIN@NOSIYA.COM	False	AQAAAAEAACcQAAAAEN9b+fKCBLZMRaLXti2gCl4HXauoim1LMj9GLe/l06/xbA2jwiaeLJg/nhxQxJLsYA==	PRW3JMATEP23MBZLF5JXXR6UPKVG7VF6	64648ab8-12b8-4ce8-8a13-ac490c86b20a	1122334455	True	False	NULL	True	0
2f29d591-89ef-45b2-89a9-08db83ceb60e	user@nosiya.com	USER@NOSIYA.COM	user@nosiya.com	USER@NOSIYA.COM	False	AQAAAAEAACcQAAAAEHeFHkR+kw6eRzy5GRhb9FFP3Dq7iLpytH1WZGUWor6hw/G14jQpgeeJj0rg0bh/DA==	M7BXJDCJKEMCFXOFVEQIHK7HLLDBK4YI	9f442b6c-af34-4441-b833-73c88421b68d	NULL	False	False	NULL	True	0

![image](https://github.com/TabakovG/NosiYa/assets/25383066/f92a5abc-d188-4ae8-9577-67b2a7d6b3e7)

5.  Go to Management Studio > Carts > Edit top 200 and paste :

1	7c34fb52-0fdb-4cd7-027f-08db822aa1b7
2	2f29d591-89ef-45b2-89a9-08db83ceb60e

![image](https://github.com/TabakovG/NosiYa/assets/25383066/18092d68-6329-4a3c-ba56-8b7a1b937fe9)

6. Go to Visual Studio > Package management console and run:

  update-database

  accountspass- 123123
   
