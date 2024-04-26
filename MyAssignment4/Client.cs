using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients {
  
  public class Client {
    
    private string _firstname;
    private string _lastname;
    private double _weight;
    private double _height;

    public Client() {
      Firstname = "xxx";
      Lastname = "xxx";
      Weight = 0;
      Height = 0;
    }

    public Client (string firstname, string lastname, double weight, double height) {
      Firstname = firstname;
      Lastname  = lastname;
      Weight = weight;
      Height = height;
    }

    public string Firstname {
			get { return _firstname; }
			set 
      {
				if (string.IsNullOrWhiteSpace(value))
					throw new ArgumentNullException("Firstname is required.");
        _firstname = value;
			}
		}

    public string Lastname {
			get { return _lastname; }
			set 
      {
				if (string.IsNullOrWhiteSpace(value))
					throw new ArgumentNullException("Lastname is required. Must not be empty or blank.");
        _lastname = value;
			}
		}
    
    public double Weight {
			get { return _weight; }
			set 
      {
				if (value < 0.0)
					throw new ArgumentException("Weight must be greater than zero (0).");
        _weight = value;
        
			}
		}

    public double Height {
			get { return _height; }
			set 
      {
				if (value < 0.0) 
					throw new ArgumentException("Weight must be greater than zero (0).");
        _height = value;
			}
		}

    public double BmiScore {
      get 
      {
        double score = (Weight / (Height * Height) * 703);
        return score;
      }
    }

    public string BmiStatus {
      get {
        string status = "";
        double bmiScore = BmiScore;

        if(bmiScore >= 0 && bmiScore <= 18.4) 
          status = "Underweight";
        if(bmiScore >= 18.5 && bmiScore <= 24.9) 
          status = "Normal";
        if(bmiScore >= 25 && bmiScore <= 39.9) 
          status = "Overweight";
        if(bmiScore >= 40)
          status = "Obese";
        
        return status;
      }
    }


    public override string ToString() {
			return $"{Firstname},{Lastname},{Weight},{Height}";
		}

    


    
  }

}