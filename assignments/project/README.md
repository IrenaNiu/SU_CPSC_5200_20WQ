# CPSC-5200-01 Individual project

Given the following specification, design the overall architecture and APIs necessary to support this application. You will be responsible for submitting architectural models, API specifications (as interface definitions, swagger, or any other reasonable format such that a developer would be able to figure out how code against your product's API).

The architecture should include:

* a high level overview of the full system
* a write-up describing any details that can't be presented in a diagram
* a discussion of how any components and connectors are built and deployed
* a discussion of any communication protocols needed
* sample code for calling your application (pick a language and show you have thought through the API usage)
* justification for your architecture
* the API specification from the client perspective
* any internal details necessary to explain how you might have implemented the service itself (ie. design patterns)

## The specification

The application that we're creating is a simple image processor. The end user provides an image in some format (your API will need to take this into account somehow) and allows the user to perform _combinations of the following operations_:

* Flip horizontal and vertical
* Rotate +/- n degrees
* Convert to grayscale
* Resize
* Generate a thumbnail
* Rotate left
* Rotate right

The user can specify which operation or operations to perform on the image. Upon completion of the transform the user should have access to the resulting image file. Operations can be applied in an order specified by the caller.

The application should be designed to be cloud-hosted. Transformation pipelines should run quickly and the caller should not have to wait an unreasonable time. You should consider storage and security issues if you keep a copy of the source or resulting image "on the service".

## Notes

The above specification is left intentionally ambiguous and open-ended. I have done this to give you latitude in your approach, but also to see which questions you may need to ask of me before you can proceed. I will answer questions about _what_ the software does; I will cover non-functional requirements as they're raised; and I will do one preliminary feedback review on your design (if you want).

## Due date and other notes

All materials are due to me by the evening of **08-Mar** (which is a Sunday). 

You have the opportunity to earn up to an _additional 10 points of extra credit_ by presenting your solution in class on 17-Mar. I will ask that you present your architecture, any interesting or difficult design details, your API, and, time permitting, do a **short demonstration of your application**. The breakdown is the in-class presentation and Q&A session is worth 5 points and a working solution that you can demo is worth an additional 5 points.
