

$(document).ready(function () {

    getList();


    //======== code for edit===========

    $(document).on('click', '#editSoftware', function () {
        Edit($(this).attr('data'));


    })


    function Edit(id) {
        var requestURL = '/Software/getsoftwareid?softwareid=' + id;
        $.ajax({
            url: requestURL,
            type: 'GET',
            success: function (response) {
                renderdata(response);
            },
            error: function () {

            }
        });

    }

    function renderdata(response) {
        $('#ID').val(response.SoftwareID);
        $('#Name').val(response.SoftwareName);
        $('#Address').val(response.Address);
        $('#rate').val(response.Rating);
        $('#btnSubmit').text("Update");
    }


    // code for submit button======

    $('#btnSubmit').click(function () {
        if (checkValidate()) {
            $('#btnSubmit').text("Processing....");
            $('#btnSubmit').prop('disabled', true);

            AddSoftware();
        }
    })


    function checkValidate() {
        var validFlag = true;

        if ($('#Name').val().trim() == '') {
            alert("Please Enter Name");
            $('#Name').focus();
            validFlag = false;
        }

        else if ($('#Address').val().trim() == '') {
            alert("Please Enter Address");
            $('#Address').focus();
            validFlag = false;
        }

        else if ($('#rate').val().trim() == '') {
            alert("Please Enter Rating");
            $('#rate').focus();
            validFlag = false;
        }
        return validFlag;

    }


    //============Addition Code========

    function AddSoftware() {
        var requestURL = '/Software/AddSoftware/';

        var SoftwareViewModel = getSoftwareViewModel();
        $.ajax({
            url: requestURL,
            type: 'POST',
            data: SoftwareViewModel,
            success: function (status) {
                alert(status.message);
                window.location.reload();
            },
            error: function () {

            }
        })
    }



    function getSoftwareViewModel() {
        var softwareViewModel = {
            SoftwareID : $('#ID').val(),
            SoftwareName: $('#Name').val(),
            Address: $('#Address').val(),
            Rating: $('#rate').val(),

            CreatedBy: 1,
        }
        return softwareViewModel;
    }



    //========= List code==========

    function getList() {
        var requestURL = '/Software/getSoftwareList';
        $.ajax({
            url: requestURL,
            type: 'GET',
            success: function (response) {
                LoadData(response);
            },
            error: function () {

            }

        })
    }

});

    //===========Load data on the list=========
    function LoadData(response) {
        var html = '';
        $.each(response, function (i, row) {
            var srno = parseInt(i) + 1;
            html += '<tr>';
            html += '<td>' + srno + '</td>';
            html += '<td>' + row.SoftwareName + '</td>';
            html += '<td>' + row.Address + '</td>';
            html += '<td>' + row.Rating + '</td>';
            html += '<td> <button class="btn btn-primary" id="editSoftware" data= ' + row.SoftwareID + '> Edit </button> </td>';
            html += '<td> <button class="btn btn-primary" id="deleteSoftwareList" data=' + row.SoftwareID + '> Delete </button> </td>';
            html += '</tr>';
        });
        $('#softwareList').html(html);
    }




    //========== Code for delete========


    $(document).on('click', '#deleteSoftwareList', function () {  
        Delete($(this).attr('data'));

    })


    function Delete(id) {
        if (confirm("Are you sure Want to delete this record ?")) {
            var requestURL = '/Software/deleteSoftware?deleteid=' + id;
            $.ajax({
                url: requestURL,
                type: 'POST',
                success: function (response) {
                    alert(response.message);
                    window.location.reload();
                },
                error: function () {

                }
            });
        }
    }











