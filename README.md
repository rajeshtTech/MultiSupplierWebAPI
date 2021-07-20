# MultiSupplierWebAPI

****************************************************** :ClientServiceApp:  ****************************************************** 
                                                        ----------------

Sample URL GET Request
http://localhost:9611/api/Consignment?Source=Mumbai&Destination=Banglore&PackageDimensions=10&PackageDimensions=20&PackageDimensions=30

============================================================================================================================================================================================
============================================================================================================================================================================================

****************************************************** :MockSupplierServerApp:  ******************************************************
                                                       ----------------------

For SupplierX

Sample URL GET Request
http://localhost:58894/api/SupplierX?{"ContactAddress":"Mumbai","WarehouseAddress":"Banglore","PackageDimensions":[10,20,30]}

Sample Response
{2000}
  
Header Info
"Supplier": "SupplierX",

Authorization 

type: "Basic"
"UserName": "ClientX",    (ClientId)
"Password": "PasswordX",  (ClientCredentials)

===============================================
For SupplierY

Sample URL GET Request
http://localhost:58894/api/SupplierY?{"Consignee":"Mumbai","Consignor":"Banglore","Cartons":[10,20,30]}

Sample Response
{2000}

Header Info
"Supplier": "SupplierY",

Authorization 

type: "Basic"
"UserName": "ClientY",    (ClientId)
"Password": "PasswordY",  (ClientCredentials)

===============================================
For SupplierZ

Sample URL GET Request
http://localhost:58894/api/SupplierZ?<ZConsignDetailsModel><Source>Mumbai</Source><Destination>Banglore</Destination><Packages><int>10</int><int>20</int><int>30</int></Packages></ZConsignDetailsModel>

Sample Response
<decimal>2000</decimal>

Header Info
"Supplier": "SupplierZ",

Authorization 

type: "Basic"
"UserName": "ClientZ",    (ClientId)
"Password": "PasswordZ",  (ClientCredentials)


============================================================================================================================================================================================
============================================================================================================================================================================================

 ****************************************************** :DATA:  ******************************************************

   {
	"Source": "Mumbai",
	"Destination": "Banglore",
	"SuppXQuoteAmt": "1000",
	"SuppYQuoteAmt": "2000",
	"SuppZQuoteAmt": "3000"
  },
  {
	"Source": "Delhi",
	"Destination": "Rajasthan",
	"SuppXQuoteAmt": "8000",
	"SuppYQuoteAmt": "4000",
	"SuppZQuoteAmt": "6000"
  },
  {
	"Source": "Punjab",
	"Destination": "Assam",
	"SuppXQuoteAmt": "8000",
	"SuppYQuoteAmt": "4000",
	"SuppZQuoteAmt": "1000"
  },
  {
	"Source": "Gujrat",
	"Destination": "UP",
	"SuppXQuoteAmt": "8000",
	"SuppYQuoteAmt": "4000",
	"SuppZQuoteAmt": "6000"
  }
