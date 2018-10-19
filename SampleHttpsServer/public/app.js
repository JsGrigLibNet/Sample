function getUrlVars() {
    var vars = [],
        hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars ;
}

var controller = getUrlVars()["link"]|| "Grid";
var gridApp = {}; 
gridApp.GetSchemaAndSettings = '/api/'+controller+'/GetSchemaAndSettings';
gridApp.GetAll = "/api/" + controller +"/GetAll";
gridApp.Put = "/api/" + controller +"/put";
gridApp.Post = "/api/" + controller +"/post";
gridApp.Delete = "/api/" + controller +"/delete";
gridApp.GetValidation = "/api/" + controller +"/GetValidation";
gridApp.UniqueId = '1';
gridApp.CreateDialogTitle = "";
gridApp.UpdateDialogTitle = "";
gridApp.SaveButtonName = "Save Changes";
gridApp.SaveButtonAttribute = "class='btn btn-primary'";

gridApp.RowOperationsName = "";
gridApp.alert = function (o) { alert(o); };
gridApp.RowOperations = [
    {
        id: "Edit",
        display: "Edit",
        handler: function (item, e) {
            gridApp.showDetailsDialog("Edit", item, gridApp.UpdateDialogTitle || "Edit");
        }
    },
    {
        id: "DeleteItem",
        display: "Delete Item",
        handler: function (item, e) {
            if (window.confirm(
                "Data will be deleted and may be IRREVERSIBLE!. Are you sure you want to do this ?")) {
                gridApp.deleteItem(item);
            } else {
                $("#jsGrid").jsGrid("cancelEdit");
            }
        }
    },
    {
        display: "Education Details",
        id: "OpenBoss",
        handler: function (item, e) {
            window.location.href = "/index.html?link=GridDetails&id=" + item.id;
        }
    }
];
gridApp.GetValidationError = function (item) {
    if (!item) {
        return "Unable to perform update. Please supply something";
    }
};

gridAppBuilder("#jsGrid", gridApp);