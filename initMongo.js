use TaskManagement
db.projects.insertOne({
    "_id" : ObjectId("6153417eda938d92c0215dc4"),
    "name" : "fakeProject",
    "sections" : [ 
        {
            "name" : "to do",
            "tasks" : []
        }, 
        {
            "name" : "in progress",
            "tasks" : []
        }, 
        {
            "name" : "done",
            "tasks" : []
        }
    ]
})
db.createUser({
    user: "TaskManagement",
    pwd: "Password1",
    roles: [
        { role: "readWrite", db:"TaskManagement"}
    ]
})