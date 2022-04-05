let userLimit = [2, 20];
let listData;
let userData;
let ownerData;
let _table;
let userInputM;

function getFormData() {
    let getFormDataSuccess = true;
    listData = $(".list-data  td > input");
    userData = $(".user-data  td > input");
    let obj = {
        Owner: {
            Name: null,
            Password: "does not matter"
        },
        List: {
            Name: null,
            Time: null
        },
        Users: []
    }

    //TODO Fix check function
    function check(field) {
        getFormDataSuccess = field == null || field == undefined || field == '' ? false : getFormDataSuccess;
    }

    obj.Owner.Name = ownerData.eq(0).val();
    check(obj.Owner.Name);
    obj.List.Name = listData.eq(0).val();
    check(obj.List.Name);
    obj.List.Time = listData.eq(1).val();
    //check(obj.List.Time);
    for (var i = 0; i < userData.length - 1; i += 2) {
        let user = {
            Name: userData.eq(i).val(),
            Mail: userData.eq(i + 1).val()
        };
        check(user.Name);
        check(user.Mail);
        obj.Users.push(user);
    }

    if (getFormDataSuccess) {
        return obj;
    }
    else {
        return null;
    }
};
function extendFormData(obj) {
    let listDataEx = $("metaData.list-data > input");
    let userDataEx = $("metaData.user-data > input");

    obj.List.OwnerID = listDataEx.eq(0).val();
    obj.List.ID = listDataEx.eq(1).val();

    for (var i = 0; i < obj.Users.length; i++) {
        obj.Users[i].ID = userDataEx.eq(i).val();
        obj.Users[i].ListID = obj.List.ID;
    }
}

//remove user
function removeUser() {
    $(".remove").unbind();
    $(".remove").click(e => {
        e.preventDefault();
        if ($(".remove").length <= userLimit[0]) {
            alert(`minimum ${userLimit[0] + 1} users`);
            return;
        }
        let elem = e.target.parentElement.classList.add("to-remove");
        $(".to-remove").remove();
    });
};

//send mails
function sendMails(listID) {
    let url = mailUrl + "?listID=" + listID;

    ajax.post(
        url,
        null,
        data => {
            alert("Email verifications are sent");
        },
        data => {
            //TODO use other method to output model state
            alert(data.responseJSON.ModelState["validationError"]);
        }
    );
}

$(() => {
    //datePicker
    $(".list-data > td > input").eq(1).datepicker({
        dateFormat: "yy-mm-dd"
    });

    //data and configurations
    ownerData = $(".host-data td > input");
    ownerData.eq(0).attr("readonly", "readonly");
    _table = $("table");
    userInputM = $(".user-data").last();

    removeUser();

    //add user
    $(".add-user").click(e => {
        e.preventDefault();
        console.log("try");
        let userInput = $("<tr>").addClass("user-data");
        userInput.html(userInputM.html());
        userInput.appendTo(_table);
        removeUser();
        console.log("catch");
    });
});