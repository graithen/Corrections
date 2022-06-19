using System.Collections.Generic;

public struct CaseData
{
    //Basic character information
    public string SecondName { get { return secondName; } set { secondName = value; } }
    private string secondName;
    public string FirstName { get { return firstName; } set { firstName = value; } }
    private string firstName;
    public int Age { get { return age; } set { age = value; } }
    private int age;
    public string Gender { get { return gender; } set { gender = value; } }
    private string gender;
    public string Occupation { get { return occupation; } set { occupation = value; } }
    private string occupation;

    //Gameplay information
    private string infraction;
    public string Infraction { get { return infraction; } set { infraction = value; } }

    private int infractionLevel;
    public int InfractionLevel { get { return infractionLevel; } set { infractionLevel = value; } }

    private List<string> infractionDetails;
    public List<string> InfractionDetails { get { return infractionDetails; } set { infractionDetails = value; } }

    private string infractionNotes;
    public string InfractionNotes { get { return infractionNotes; } set { infractionNotes = value; } }
}