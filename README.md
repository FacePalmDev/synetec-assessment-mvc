# Synetec Basic ASP.Net MVC assessement

## How I approached this task. 
I've been a little busy lately (🎈It's my birthday today! Woooop!🎈). I've dipped in and out of this task over the last week.

## Step 1 - Writing Tests
In true TDD fashion I began by writing tests. Though this increased my maintanance overhead (it took 2-3 times longer to break 
the application up into an N-Tier app). I thought that this was a mistake whilst fixing it but in hind sight I still think it was 
worth the effort to ensure that I wasn't breaking existing functionality. 

## Step 2 - Separating Concerns by Restructuing The Application 

As I mentioned earlier I restructured the application into a class 3-Teir model architecture (Domain, Core and Data layers). 

### 2.1 Dependency Injection
I chose structuremap for this as I like it's built in conventions where you don't need to explicitly specify every single mapping. I favoured convention
over configuration here and this also reduces the code base just a tad.

### 2.2 Automapper

I implemented automapper for transfering information between layers. Automapper enabled me to map my DTOs to Domain Models to ViewModels (and vise versa).
This is useful because it allowed me to fix naming conventions and restructure models for the most appropriate useage in each layer.

## Step 3 - FEATURE SUGGESTION Created a Harness for Parallel Processing using Vector Maths

I had an idea in my mind from the get go that I initially had to put to one side. I wanted to implement this next before I forgot about 
the idea completely. I realised that multiple users could have their bonuses efficiently calculated in parallel using Vectors maths.

I wasn't asked to do this so I didn't prioritise this but I put together a harness with the intention of 
using to communicate my ideas with the team when suggesting this as a future improvement. 

I've been working with a lot Matrix maths as part of my personal machine learning hobby projects so I drew on this transferable knowledge to 
make this feature suggestion. 

I spent a little time working calculations out on paper, comparing them to the current outputs and then using that 
to test my a new calculation mechanism purely on paper.

Here's the main working code, as you can see it's very neat and easy to read as well as being performant:

``` csharp 

var totalBonus = profit / 100 * percentOfProfitBonus;
var wagesVector = Vector<Double>.Build.Dense(salaries);
var totalWageBudget = wagesVector.Sum();
var wageBudgetPercentages = 100 / totalWageBudget * wagesVector;
var bonusCalculations = totalBonus / 100 * wageBudgetPercentages;

return bonusCalculations.ToArray();

```

For a working example please see the harness. 

But, for now, let's get back to the task at hand...

## 3. Implemented Structured Logging Using Serilog

Logging can be configured in the web.config file at runtime without changing the application code. 
Both Windows Event Logging and rolling file logging are optional.

> Note: Windows Event Logging requires admin rights so I disabled this in favour of rolling file logging.

## 4. Added Validation and Data type changes.
I thought it was worth doing to ensure that nobody could inject negative numbers into our system
via an malicious request. 

Where numbers must be postive I chose unsigned integers too.

