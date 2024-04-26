using Clients;

Client clientDetails = new();
List<Client> clientList = [];

bool valid = true;
LoadFileToMemory(clientList);

Console.Clear();
while(valid) 
{
  try 
  {
    displayMainMenu();
    string mainMenuChoice = Prompt("\nEnter a Main Menu Choice: ").ToUpper();
    if(mainMenuChoice == "L")
      displayAllClients();
    if(mainMenuChoice == "F")
      findClient();
    if(mainMenuChoice == "A")
      AddClientToList(clientList);
    if(mainMenuChoice == "E")
      EditClient();
    if(mainMenuChoice == "D")
     DeleteClient();
    if(mainMenuChoice == "S")
      showClientBmiInfo();
    if (mainMenuChoice == "Q") 
    {
      SaveClients();
			valid = false;
			throw new Exception("Have a great day.");

    }

  }  
  catch (Exception ex) 
  {
    Console.WriteLine(ex.Message);
  } 
}

void displayMainMenu() 
{
  Console.WriteLine($"\n MENU OPTIONS");  
  Console.WriteLine($"(L)ist of clients");
  Console.WriteLine($"(F)ind Client");
  Console.WriteLine($"(A)dd New Client");
  Console.WriteLine($"(E)dit Client details");
  Console.WriteLine($"(D)elete Client Record");
  Console.WriteLine($"(S)how Client BMI Information");
  Console.WriteLine($"(Q)uit");
}

void displayEditMenu() 
{
  Console.WriteLine($"(F)irst Name");
  Console.WriteLine($"(L)ast Name");
  Console.WriteLine($"(H)eight");
  Console.WriteLine($"(W)eight");
  Console.WriteLine($"(R)eturn to Main Menu");
}


double PromptDoubleBetweenMin(String msg, double min)
{
	double num = 0;
	while (true)
	{
		try
		{
			Console.Write($"{msg}: ");
			num = double.Parse(Console.ReadLine());
			if (num < min)
				throw new Exception($"Must be greater than {min:n2}");
			break;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Invalid: {ex.Message}");
		}
	}
	return num;
}

string Prompt(string prompt)
{
	string myString = "";
	while (true)
	{
		try
		{
		Console.Write(prompt);
		myString = Console.ReadLine().Trim();
		if(string.IsNullOrEmpty(myString))
			throw new Exception($"Empty Input: Invalid input.");
		break;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}
	return myString;
}


void displayAllClients()
{
  try 
  {
    if(clientList.Count <= 0)
      throw new Exception($"No data has been loaded");
    
    foreach(Client clientDetails in clientList) 
      showClientInfo(clientDetails);

  } catch (Exception ex) {
    Console.WriteLine($"Error: {ex.Message}");
  }
}



void LoadFileToMemory(List<Client> clientList)
{
  while(true)
  {
    try
    {
			string fileName = "clientDetails.csv";
			string filePath = $"./data/{fileName}";
			if (!File.Exists(filePath))
				throw new Exception($"The file {fileName} does not exist.");
			string[] csvFileInput = File.ReadAllLines(filePath);
			for(int i = 0; i < csvFileInput.Length; i++)
			{
				string[] items = csvFileInput[i].Split(',');
				
				Client clientDetails = new(items[0], items[1], double.Parse(items[2]), double.Parse(items[3]));
        clientList.Add(clientDetails);
			}
			Console.WriteLine($"Load complete. {fileName} has {clientList.Count} data entries");
			break;
    }
    catch(Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
  }
}

void SaveClients() {
      string fileName = "clientDetails.csv";
      string filePath = $"./data/{fileName}";
      List<String> ClientRecords = [];
      foreach(Client data in clientList) {
        ClientRecords.Add($"{data.Firstname}, {data.Lastname}, {data.Weight}, {data.Height}");
      }
      File.WriteAllLines(filePath, ClientRecords);
    }

void showClientBmiInfo()
{
  
  string clientDetailsName = Prompt("\nEnter clientDetails's Firstname: ");
  List<Client> filteredClient = clientList.Where(c => c.Firstname.Contains(clientDetailsName)).ToList();
  Client selectedClient = filteredClient.FirstOrDefault();
  Console.WriteLine($"\n{selectedClient.ToString()}");
  Console.WriteLine($"Client's BMI Score     :\t{selectedClient.BmiScore:n2}");
	Console.WriteLine($"Client's BMI Status    :\t{selectedClient.BmiStatus}");
}

void showClientInfo(Client clientDetails)
{
  if(clientDetails == null)
    throw new Exception("No Client In Memory");
  Console.WriteLine($"\n{clientDetails.ToString()}");
  Console.WriteLine($"Client's BMI Score     :\t{clientDetails.BmiScore:n2}");
	Console.WriteLine($"Client's BMI Status    :\t{clientDetails.BmiStatus}");
}

void AddClientToList(List<Client> clientList)
{
  GetFirstname(clientDetails);
  GetLastname(clientDetails);
  GetWeight(clientDetails);
  GetHeight(clientDetails);
  clientList.Add(clientDetails);


}

void findClient()
{
  displayAllClients();
  string clientDetailsName = Prompt("Enter clientDetails's Firstname: ");
  List<Client> filteredClient = clientList.Where(c => c.Firstname.Contains(clientDetailsName)).ToList();

  Client selectedClient = filteredClient.FirstOrDefault();
  Console.WriteLine($"\n{selectedClient.ToString()}");
  Console.WriteLine($"Client's BMI Score     :\t{selectedClient.BmiScore:n2}");
	Console.WriteLine($"Client's BMI Status    :\t{selectedClient.BmiStatus}");

}


void EditClient() 
{
      
      displayAllClients();
  string clientDetailsName = Prompt("Enter clientDetails's Firstname: ");
    List<Client> filteredClient = clientList.Where(c => c.Firstname.Contains(clientDetailsName)).ToList();
    Client selectedClient = filteredClient.FirstOrDefault();
  
      while(true) 
      {
        Console.WriteLine($"===== SELECT DATA OF TO EDIT ====="); 
        displayEditMenu();
        string editChoice = Prompt("\nEnter Edit Menu Choice: ").ToUpper();
        if(editChoice == "F") 
        {
          selectedClient.Firstname = Prompt($"Enter Client Firstname: ");
        } else if(editChoice == "L") 
        {
          selectedClient.Lastname = Prompt($"Enter Client Lastname: ");
        } else if(editChoice == "W") 
        {
          selectedClient.Weight = PromptDoubleBetweenMin($"Enter Client Weight (lbs): ", 0);
        } else if(editChoice == "H")
         {
          selectedClient.Height = PromptDoubleBetweenMin($"Enter Client Height (inches): ", 0);
        }else if(editChoice == "R") 
        {
          Console.WriteLine($"You have successfully updated details");
          break;
        } else 
        {
          throw new Exception("Invalid Edit Menu Choice. Please Try Again.");
        }
      }          
    }



void DeleteClient() 
{
      
      displayAllClients();
  string clientDetailsName = Prompt("Enter clientDetails's Firstname: ");
  List<Client> filteredClient = clientList.Where(c => c.Firstname.Contains(clientDetailsName)).ToList();
  Client selectedClient = filteredClient.FirstOrDefault();
  
      while(true) 
      {
        string confirmation = Prompt($"You are about to delete "+ selectedClient.Firstname + "'s record. Proceed? Y/N: ").ToUpper();
        if (confirmation == "Y") 
        {
          clientList.Remove(selectedClient);
          Console.WriteLine($"{selectedClient.Firstname}'s has been deleted.");
          break;
        } else if (confirmation == "N") 
        {
          Console.WriteLine($"Delete operation cancelled for {selectedClient.Firstname}.");
          break;
        } else 
        {
          Console.WriteLine($"Invalid confirmation input. Please enter 'Y' or 'N'.");
        }
      }
    }


void GetFirstname(Client clientDetails)
{
	string myString = Prompt($"Enter Firstname: ");
	clientDetails.Firstname = myString;
}

void GetLastname(Client clientDetails)
{
	string myString = Prompt($"Enter Lastname: ");
	clientDetails.Lastname = myString;
}

void GetWeight(Client clientDetails)
{
	double myDouble = PromptDoubleBetweenMin("Enter Weight in inches: ", 0);
	clientDetails.Weight = myDouble;
}

void GetHeight(Client clientDetails)
{
	double myDouble = PromptDoubleBetweenMin("Enter Height in inches: ", 0);
	clientDetails.Height = myDouble;
}