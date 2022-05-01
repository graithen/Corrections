using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseStruct
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

    //Gameplay information
    string Infraction { get { return infraction; } set { infraction = value; } }
    public string infraction;
}
