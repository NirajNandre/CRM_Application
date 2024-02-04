$(document).ready(function () {

    if (window.location.href.toLowerCase().indexOf('oncallhours/add') > -1) {

        if (window.location.href.indexOf('OnCallId') > -1) {

            var arrExistingDays = $('#Days').val().split(',').map(function (v) {
                return parseInt(v, 10);
            });
            $('#weekdays').weekdays(
                {
                    selectedIndexes: arrExistingDays
                }
                );
            $('.fromTimePicker').wickedpicker({
                twentyFour: true,
                title: 'On Call From Time',
                now: $('#FromTime').val()
            });
            $('.toTimePicker').wickedpicker({
                twentyFour: true,
                title: 'On Call To Time',
                now: $('#ToTime').val()
            });
        }
        else {
            $('#weekdays').weekdays();
            $('.fromTimePicker').wickedpicker({
                twentyFour: true,
                title: 'On Call From Time',
            });
            $('.toTimePicker').wickedpicker({
                twentyFour: true,
                title: 'On Call To Time',
            });
        }
    }
    if (window.location.href.toLowerCase().indexOf('/application/add') > 0) {
        $.ajax({
            url: "/CommonData/GetAllMembers",
            dataType: "json",
            asyc: false,
            success: function (data) {

                $('.memberlookup').autocomplete({
                    source: data,
                    select: function (event, ui) {
                       
                        $(this).next().val(ui.item.data);
                    }
                });
            }
        });
        $.ajax({
            url: "/CommonData/GetAllAppMembers",
            dataType: "json",
            asyc: false,
            success: function (data) {

                $('.appmemberlookup').autocomplete({
                    source: data,
                    select: function (event, ui) {                        
                        $(this).next().val(ui.item.data);
                        $(this).next().next().val(ui.item.value);
                    }
                });
            }
        });
    }
    //Code for Autocomplete Application Name on Client Page to Search application Name Textbox
    if (window.location.href.toLowerCase().indexOf('/client/add') > 0) {
        $.ajax({
            url: "/CommonData/GetAllApplications",
            dataType: "json",
            asyc: false,
            success: function (data) {

                $('.applicationlookup').autocomplete({
                    source: data,
                    select: function (event, ui) {

                        $(this).next().val(ui.item.data);
                    }
                });
            }
        });
        
    }

    //Script for Add Button to add Application in the list on Client Add/Edit Page
   

    if (window.location.href.toLowerCase().indexOf('/oncallhours/add') > 0 || window.location.href.toLowerCase().indexOf('/applicationmessage/add') > 0) {
        if ($('#DownMessageId').val() != "0") {
            $('.optionalSection').css('display', 'none');
            $('.optionalField').removeAttr('required');
        }
        $.ajax({
            url: "/CommonData/GetAllApplications",
            dataType: "json",
            asyc: false,
            success: function (data) {

                $('.memberlookup').autocomplete({
                    source: data,
                    select: function (event, ui) {
                        $(this).next().val(ui.item.data);
                        $('#hdnAppName').val(ui.item.value);
                    }
                });
            }
        });


        $('#DownMessage_EmailSubject').focus(function () {
            var appId = $('#ApplicationId').val();
            $.ajax({
                url: "/CommonData/GetAllApplicationMessages?appId=" + appId,
                dataType: "json",
                asyc: false,
                success: function (data) {

                    $('#DownMessage_EmailSubject').autocomplete({
                        source: data,
                        select: function (event, ui) {
                            $(this).next().val(ui.item.data);
                            $('#hdnDwonMsgName').val(ui.item.value);
                        }
                    });

                }
            });
        });

        $('#DownMessage_EmailSubject').blur(function () {
            var hdnDownMsgid = $('#DownMessageId');
            if ($(this).val() == '') {
                hdnDownMsgid.val('');
                $('.optionalSection').css('display', 'flex');
                $('.optionalField').attr('required', 'required');
            }
            else {
                $('.optionalSection').css('display', 'none');
                
                $('.optionalField').removeAttr('required');
            }
        });
    }
    $('#TicketCreationOption').change(function () {
        if ($(this).val() == "1") {
            $('#spnInterval').css('visibility', 'visible');
            $('#TicketCreationInterval').attr('requried', 'required');

            $('#spnGroupBy').css('visibility', 'hidden');

        }
        else if ($(this).val() == "2") {
            $('#spnGroupBy').css('visibility', 'visible');
            $('#TicketCreationInterval').removeAttr('required');

            $('#spnInterval').css('visibility', 'hidden');
        }
        else {
            $('#TicketCreationInterval').removeAttr('required');

            $('#spnInterval').css('visibility', 'hidden');

            $('#spnGroupBy').css('visibility', 'hidden');
        }
    });
    $('.weekdays-list>li').click(function () {
        var selectedDays = $('#Days').val().length > 0 ? $('#Days').val().split(',') : [];
        var newDay = $(this).attr('data-day');
        if ($(this).hasClass('weekday-selected')) {
            if (selectedDays.indexOf(newDay) == -1) {
                selectedDays.push(newDay);
            }
        }
        else {
            if (selectedDays.indexOf(newDay) > -1) {

                selectedDays.splice(selectedDays.indexOf(newDay), 1);
            }
        }
        var strdays = selectedDays.join(','); //selectedDays.length > 1 ? selectedDays.join(',').substring(1) : selectedDays.join(',');
        $('#Days').val(strdays);
        $('#txtDays').val(strdays);
    });

    if ($('#checkPassword').length == 0) {
        $('.passworField').css('display', 'flex');
        $('#Password').attr('required', 'required')
        $('#Password').attr('minlength', '6');
        $('#ConfirmPassword').attr('required', 'required');    
    }
    else 
         {
            $('.passworField').css('display', 'none');
            $('#Password').removeAttr('required')
            $('#Password').removeAttr('minlength');
            $('#ConfirmPassword').removeAttr('required');
        }
    $('#checkPassword').change(function () {

        if ($(this).prop('checked')) {
            $('.passworField').css('display', 'flex');
            $('#Password').attr('required', 'required')
            $('#Password').attr('minlength', '6');
            $('#ConfirmPassword').attr('required', 'required');
        } else {
            $('.passworField').css('display', 'none');
            $('#Password').removeAttr('required')
            $('#Password').removeAttr('minlength');
            $('#ConfirmPassword').removeAttr('required');

        }
    });

    $('#btnEmployeeMasterSubmit').click(function () {
        
        $('#EmployeeName').attr('required', 'required')
        $('#EmployeeName').attr('minlength', '6');
        $('#EmployeeId').attr('required', 'required')
        $('#DateOfJoining').attr('required', 'required')

    });

    $('#btnAddMember').click(function () {
        $('#addMemberError').html('');
        var name = $('#txtMemberName').val().trim();
        var id = $('#hdnMemberId').val().trim();
        if (name != '' && id != '') {
            $.ajax({
                url: "/Application/AddMember",
                dataType: "json",
                method: "POST",
                data: jQuery.param({ name: name, memberId: id, action: 'AddMember' }),
                asyc: false,
                success: function (data) {
                    DisplayMemberTable(data);
                    $('#txtMemberName').val('');
                    $('#hdnMemberId').val('');

                    $.ajax({
                        url: "/CommonData/GetAllAppMembers",
                        dataType: "json",
                        asyc: false,
                        success: function (data) {

                            $('.appmemberlookup').autocomplete({
                                source: data,
                                select: function (event, ui) {
                                    $(this).next().val(ui.item.data);
                                    $(this).next().next().val(ui.item.value);
                                }
                            });
                        }
                    });
                }
            });
        }
        else {
            $('#addMemberError').html('Please select a member to add.');
        }
    });

    $('#btnAddApplication').click(function () {
        $('#addApplicationError').html('');
        var name = $('#txtApplicationName').val().trim();
        var id = $('#hdnApplicationId').val().trim();
        if (name != '' && id != '') {
           
            $.ajax({
                url: "/Client/AddApplication",
                dataType: "json",
                method: "POST",
                data: jQuery.param({ name: name, applicationId: id, action: 'AddApplication' }),
                asyc: false,
                success: function (data) {
                 
                    DisplayApplicationTable(data);
                    $('#txtApplicationName').val('');
                    $('#hdnApplicationId').val('');



                }
            });
        }
        else {
            $('#addApplicationError').html('Please select a application to add.');
        }
    });

    $('.delete').click(function () {
        if (confirm('Are you sure, you want to delete?')) {
            var recId = $(this).attr('title');
            var res = $('#hdnResourceName').val();
            $.ajax({
                url: "/CommonData/DeleteResource",
                dataType: "json",
                method: "POST",
                data: jQuery.param({ id: recId, resource: res }),
                asyc: false,
                success: function (data) {
                    if (data == 1) {
                        window.location.href = window.location.href;
                    }
                    else if (data == -1) {
                        alert('Member has been used in Application, please remove the mapping first!');
                    }
                    else {
                        alert('Some error occured while deleting record, please try again!');
                    }
                },
                error: function (xhr, status, error) {
                    console.log(error);
                }

            });

        }


    });

    //calling DataTable method on mdbootstrap table
    $('#mdbootstraptable').DataTable({
        "paging": true, // false to disable pagination
        "pagingType": "full_numbers" //options available: simple, numbers, simple_numbers, full, full_numbers, first_last_numbers
    });

});
function DisplayMemberTable(data) {
    $('#tblMembers>tbody').empty();
    $.each(data, function (index) {
        var tr = '<tr>';
        tr += '<td>' + data[index].MemberName + '</td>';
        tr += '<td><ul class="list-inline m-0">';
        tr += '<li class="list-inline-item">';
        tr += '<span title="Remove">';
        tr += '<a onclick="removeMember(' + data[index].MemberId + ')">';
        tr += '<i class="fa fa-trash">';
        tr += '</i>';
        tr += '</a>';
        tr += '</span>';
        tr += '</li>';
        tr += '</ul></td>';
        $('#tblMembers>tbody').append(tr);
    });
}

function DisplayApplicationTable(data) {
    $('#tblApplications>tbody').empty();
    $.each(data, function (index) {
        var tr = '<tr>';
        tr += '<td>' + data[index].ApplicationName + '</td>';
        tr += '<td><ul class="list-inline m-0">';
        tr += '<li class="list-inline-item">';
        tr += '<span title="Remove">';
        tr += '<a onclick="removeApplication(' + data[index].ApplicationId + ')">';
        tr += '<i class="fa fa-trash">';
        tr += '</i>';
        tr += '</a>';
        tr += '</span>';
        tr += '</li>';
        tr += '</ul></td>';
        $('#tblApplications>tbody').append(tr);
    });
}
function removeMember(memberId) {
    $('#addMemberError').html('');
    if (confirm('Are you sure, you want to remove member?')) {
        $.ajax({
            url: "/Application/AddMember?name=-&memberId=" + memberId + "&action=RemoveMember",
            dataType: "json",
            method: "POST",
            data: jQuery.param({ name: '-', memberId: memberId, action: 'RemoveMember' }),
            asyc: false,
            success: function (data) {
                DisplayMemberTable(data);
                $('#txtMemberName>tbody').val('');
                $('#hdnMemberId').val('');
                $.ajax({
                    url: "/CommonData/GetAllAppMembers",
                    dataType: "json",
                    asyc: false,
                    success: function (data) {

                        if ($('#ServiceLeadId').val() == memberId) {
                            $('#ServiceLead_MemberName').val('');
                            $('#ServiceLeadId').val('');
                        }
                        if ($('#CSMId').val() == memberId) {
                            $('#CSM_MemberName').val('');
                            $('#CSMId').val('');
                        }
                        if ($('#SDMId').val() == memberId) {
                            $('#SDM_MemberName').val('');
                            $('#SDMId').val('');
                        }
                        if ($('#DefaultAssigneeId').val() == memberId) {
                            $('#DefaultAssignee_MemberName').val('');
                            $('#DefaultAssigneeId').val('');
                        }

                        $('.appmemberlookup').autocomplete({
                            source: data,
                            select: function (event, ui) {
                                $(this).next().val(ui.item.data);
                            }
                        });
                    }
                });
            }

        });
    }

}

function removeApplication(applicationId) {
    $('#addApplicationError').html('');
    if (confirm('Are you sure, you want to remove application?')) {
        $.ajax({
            url: "/Client/AddApplication?name=-&applicationId=" + applicationId + "&action=RemoveApplication",
            dataType: "json",
            method: "POST",
            data: jQuery.param({ name: '-', applicationId: applicationId, action: 'RemoveApplication' }),
            asyc: false,
            success: function (data) {
                DisplayApplicationTable(data);
                $('#txtApplicationName>tbody').val('');
                $('#hdnApplicationId').val('');
                
            }

        });
    }


}