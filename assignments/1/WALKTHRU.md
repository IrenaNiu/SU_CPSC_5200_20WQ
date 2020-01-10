# REST example walkthru

Show the root document, this will tell us what 'applications' are installed, and whether the one we want, 'timesheets' is available.

```bash
curl http://localhost:5000/ --dump-header -
```

Looks like the timesheets application is there. Let's go ahead and query that. We should get back a list of the known timesheets (in this case it's empty because we just restarted the 'database')

```bash
curl http://localhost:5000/timesheets --silent | jq .
```

Ok, let's create our first timesheet. We'll assume for this discussion the the employee's id is `1`. Later on we'll add another person.

```bash
curl http://localhost:5000/timesheets \
    -X POST \
    --header "Content-Type: application/json" \
    --data '{ "id": 1 }' \
    --dump-header -
```

Cool, now we have the empty sheet equivalent of a timesheet. That is, we've written our name and date on the paper. Notice that the status is `draft` which is exactly what we expect from the state transition model.

Just to make sure things are working, let's go get that timesheet. We can tell how by looking at the document, it has a `self` element that points back at, well, itself.

```bash
curl http://localhost:5000/timesheets/554dbcc2-e532-46d7-abc5-8dc3da3abc07
```

But, you might notice at this point that there aren't any `lines` or `transitions` in the timecard. The only thing that's in there is the `documentation` element. Well, per the API that's what we want. Let's ask for some lines

```bash
curl http://localhost:5000/timesheets/554dbcc2-e532-46d7-abc5-8dc3da3abc07/lines
```

...and some transitions (which we'll have, because that's what we expect the application to do).

```bash
curl http://localhost:5000/timesheets/554dbcc2-e532-46d7-abc5-8dc3da3abc07/transitions
```

Next, let's add some time to our timesheet. This is done by creating timesheet lines

```bash
curl http://localhost:5000/timesheets/554dbcc2-e532-46d7-abc5-8dc3da3abc07/lines \
    -X POST \
    --header "Content-Type: application/json" \
    --data '{ "week": 2, "year": 2018, "day": "wednesday", "hours": 8, "project": "CPSC 5200" }' \
    --dump-header -

curl http://localhost:5000/timesheets/554dbcc2-e532-46d7-abc5-8dc3da3abc07/lines \
    -X POST \
    --header "Content-Type: application/json" \
    --data '{ "week": 2, "year": 2018, "day": "thursday", "hours": 8, "project": "CPSC 5200" }' \
    --dump-header -

curl http://localhost:5000/timesheets/554dbcc2-e532-46d7-abc5-8dc3da3abc07/lines \
    -X POST \
    --header "Content-Type: application/json" \
    --data '{ "week": 2, "year": 2018, "day": "friday", "hours": 8, "project": "CPSC 5200" }' \
    --dump-header -
```

Now, let's submit

```bash
curl http://localhost:5000/timesheets/554dbcc2-e532-46d7-abc5-8dc3da3abc07/submittal \
    -X POST \
    --header "Content-Type: application/json" \
    --data '{ "submitter": 1 }' \
    --dump-header -
```

Then we'll reject

```bash
curl http://localhost:5000/timesheets/554dbcc2-e532-46d7-abc5-8dc3da3abc07/rejection \
    -X POST \
    --header "Content-Type: application/json" \
    --data '{ "rejecter": 2 }' \
    --dump-header -
```
