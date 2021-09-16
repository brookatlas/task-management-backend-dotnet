use TaskManagement
db.projects.insertOne({ "name": "fakeProject", "tasks": []})
db.createUser({
    user: "TaskManagement",
    pwd: "Password1",
    roles: [
        { role: "readWrite", db:"TaskManagement"}
    ]
})