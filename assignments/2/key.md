# Assignment 2

Assignment 2 asked you to consider the relationships between _architectural styles_ and _architectural patterns_. As you've noticed, there is no real consensus around this. So, let's take it apart and see what we get.

[Taylor](#taylor) uses the following definitions:

> An _Architectural Pattern_ is a named collection of architectural design decisions that are applicable to a recurring design problem parameterized to account for different software development contexts in which that problem appears.

and

> An _Architectural Style_ is a named collection of architectural design decisions that (1) are applicable in a given development context, (2) constrain architectural design decisions that are specific to a particular system within that context, and (3) elicit beneficial qualities in each resulting system.

That doesn't really help though. What Taylor is saying here is that the style defines the common structure to an architecture (i.e. it's a shared memory, messaging, or distributed system) and that the pattern defines the more domain-specific solution to the problem.

I made a point in class that an architectural pattern is analogous to a spoken or written language's grammar in that it describes the rules and structures of the common language, and that architectural style is more akin to the dialect or accent with which that language is used. In this model the relationship between the two is reversed.

Architectural patterns are patterns used in the coarse-grained design and establishment of architectures. Design patterns extend and refine the coarse-grained design, and are also used in the more detailed design. An architectural pattern is on a higher level of abstraction (more abstract implies less specific or farther from the domain) than a design pattern and has a greater impact on the architecture as a whole. It is still, however, something that presents a solution to a specific recurring problem, as opposed to a style that is rather a categorization of architecture. [POSA](#posa) makes a distinction between styles, architecture patterns, design patterns and so-called idioms. Styles are very similar to architectural patterns, but differ in some important respects:

* Styles describe only the overall structural framework for applications, while architectural patterns define the basic structure of an application
* Styles are independent of each other, but a pattern depends on the smaller patterns it contains and on the larger patterns in which it is contained
* Patterns are more problem-oriented than styles. Styles are independent of a specific design situation

Then we have the model proposed by [McGovern](#mcgovern), which turns out to be very nuanced.

* An architectural style is a central, organizing concept for a system.
* An architectural pattern describes a coarse-grained solution at the level of subsystems or modules and their relationships.

And, finally, [Microsoft](#microsoft) seemingly punts on the whole thing and just considers the two terms as synonymous.

>An architectural style, sometimes called an architectural pattern, is a set of principles

But, then they go into some detail on how to talk about them by discussing categories and grouping of the styles.

|Category|Architecture styles|
|---|---|
|Communication|Service-Oriented Architecture (SOA), Message Bus|
|Deployment|Client/Server, N-Tier, 3-Tier|
|Domain|Domain Driven Design|
|Structure|Component-Based, Object-Oriented, Layered Architecture|

and

|Architecture style|Description|
|---|---|
|Client/Server|Segregates the system into two applications, where the client makes requests to the server. In many cases, the server is a database with application logic represented as stored procedures.|
|Component-Based Architecture|Decomposes application design into reusable functional or logical components that expose well-defined communication interfaces.|
|Domain Driven Design|An object-oriented architectural style focused on modeling a business domain and defining business objects based on entities within the business domain.|
|Layered Architecture|Partitions the concerns of the application into stacked groups (layers).|
|Message Bus|An architecture style that prescribes use of a software system that can receive and send messages using one or more communication channels, so that applications can interact without needing to know specific details about each other.|
|N-Tier / 3-Tier|Segregates functionality into separate segments in much the same way as the layered style, but with each segment being a tier located on a physically separate computer.|
|Object-Oriented|A design paradigm based on division of responsibilities for an application or system into individual reusable and self-sufficient objects, each containing the data and the behavior relevant to the object.|
|Service-Oriented Architecture (SOA)|Refers to applications that expose and consume functionality as a service using contracts and messages.|

So, for CPSC-5200 we're just going to take the same approach. _Architecture styles_ and _architecture patterns_ are simply ways to describe the overall structure of a given solution to a problem. The application problem domain has no direct impact on the architecture that we chose. That is, it doesn't matter if we're building an ERP, HRIS, inventory management, or CRM application, the architecture that we choose will be determined by the non-functional requirements.

However, that's not to say that some application models won't drive some architectural decisions. Building an embedded system in a limited processor, RAM, and connectivity environment will necessarily drive architectural choices, and even the overall architecture (pattern or style). But, note that those are, again, non-functional constraints we've placed on the system and do not talk about the application feature (the _functional_ requirements).

<a name="taylor">[taylor]</a>: Taylor, R. N., Medvidovic, N., & Dashofy, E. M. (2010). Software architecture: foundations, theory, and practice. Hoboken: John Wiley & Sons.

<a name="posa">[posa]</a>: Buschmann et al, Pattern Oriented Software Architecture – A System of Patterns Vol 1, Wiley 1996

<a name="mcgovern">[mcgovern]</a>: McGovern et al, A Practical Guide to Enterprise Architecture, Prentice Hall 2003

<a name="microsoft">[microsoft]</a>: Microsoft® Application Architecture Guide, 2nd Edition, Microsoft Press 2009
