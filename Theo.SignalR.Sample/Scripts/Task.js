var clickCounter = 0;
$(document).ready(function () {

    var taskHub = $.connection.taskHub,
        table = $('#taskTable'),
        loading = $('.loading');
    alert = $("#stopAlert");

    function setUpEventandler() {
        $('#AutoUpdate').click(function () {
            if (clickCounter == 0) {
                $('#AutoUpdate').attr("value", "Stop Auto update task status");
                taskHub.server.updateTasks();
                clickCounter = 1;
            }
            else {
                $('#AutoUpdate').attr("value", "Auto update task status");
                taskHub.server.stopUpdateTasks();
                clickCounter = 0;
            }
        });
    }

    function init() {
        $.connection.hub.logging = true;
        return taskHub.server.getTasks().done(function (data) {
            loading.hide();
            alert.hide();
            setUpEventandler();

            table.dataTable({
                "data": data,
                scrollY: 300,
                paging: false,

                "columns": [
                    { "data": "TaskID" },
                    { "data": "Name" },
                    { "data": "Owner" },
                    { "data": "Done" },
                ],
                "columnDefs": [
                    {
                        "targets": 0,
                        "visible": false
                    },
                    {
                        "render": function (rowdata, type, row) {
                            var cls = rowdata ? "done" : "notdone";
                            return "<div class='status' " + cls + "></div>"
                        },
                        "tragets": 3
                    }
                ]
            })
        });
    }

    $.extend(taskHub.client, {
        updateTaskStatus: function (task) {
            var dt = table.dataTable();
            if (task) {
                console.log("Updating the Task ID " + task.TaskID);
                dt.fnUpdate(task, task.TaskID);
            }
            else {
                console.log("The Task returned was null ");
            }

        },
        stoppedTaskStatus: function () {
            $('#AutoUpdate').attr("value", "Auto update task status");
            alert.show();
            clickCounter = 0;
        }
    });

    $.connection.hub.start().then(init);

});