(function () {

    $(function () {
      
        $('#firstName').focus();
        
        $('#spnError').hide();
        $('#spnSpareError').hide();


        $('.datepicker').datepicker({
            format: 'dd/mm/yyyy',            
        });
             

        $('#btnTicket').click(function (e) {

            if (validateInput() !== true)
                return false;
                      
            $('#spnError').html("");

            e.preventDefault();

            abp.ui.setBusy(
                $('#ticketForm'),
                abp.ajax({
                    url: abp.appPath + 'Professional/SaveProfTicket',
                    type: 'POST',
                    data: JSON.stringify({

                        FirstName: $('#firstName').val(),
                        LastName: $('#lastName').val(),
                        Address: $('#address').val(),
                        City: $('#city').val(),
                        Zipcode: $('#zipcode').val(),
                        MobileNumber: $('#mobileNumber').val(),
                        Email: $('#emailId').val(),
                        FileName: $('#fileName').val(),
                        SpareFileName: $('#fileSpareName').val(),
                        Version:"French"
                    })
                })
                .done(function (data) {
                                       
                    if (data > 0) {
                        $('#confirmationModel').modal();
                        abp.notify.info('Saved Successfully');
                    }
                    else {
                        $('#spnError').html(data);
                        $('#errorModel').modal();
                    }
                                           
                    
                })
                
            );
                        
        });
        

        $('#fileUpload').change(function () {
  
            // Checking whether FormData is available in browser  
            if (window.FormData !== undefined) {  
  
                var fileUpload = $("#fileUpload").get(0);
                var files = fileUpload.files;  
              
                // Create FormData object  
                var fileData = new FormData();  
  
                // Looping over all files and add it to FormData object  
                for (var i = 0; i < files.length; i++) {  
                    fileData.append(files[i].name, files[i]);  
                }  
                
                $.ajax({  
                    url: '/Professional/UploadFiles',
                    type: "POST",  
                    contentType: false, // Not to set any content header  
                    processData: false, // Not to process data  
                    data: fileData,  
                    success: function (data) {
                       
                        $('#fileName').val(data.result.fileName)
                        $('#spnError').hide();
                        
                    },  
                    error: function (err) {  
                        abp.notify.info('Only Excel file can be uploaded');
                    }  
                });  
            } else {  
                alert("FormData is not supported.");  
            }  
        });


        $('#fileUploadSpare').change(function () {

            // Checking whether FormData is available in browser  
            if (window.FormData !== undefined) {

                var fileUpload = $("#fileUploadSpare").get(0);
                var files = fileUpload.files;

                // Create FormData object  
                var fileData = new FormData();

                // Looping over all files and add it to FormData object  
                for (var i = 0; i < files.length; i++) {
                    fileData.append(files[i].name, files[i]);
                }

                $.ajax({
                    url: '/Professional/UploadFiles',
                    type: "POST",
                    contentType: false, // Not to set any content header  
                    processData: false, // Not to process data  
                    data: fileData,
                    success: function (data) {

                        $('#fileSpareName').val(data.result.fileName)
                        $('#spnSpareError').hide();
                    },
                    error: function (err) {
                        abp.notify.info('Only Excel file can be uploaded');
                    }
                });
            } else {
                alert("FormData is not supported.");
            }
        });
        
       
        $('#btnConfirm').click(function () {
            window.location.href = '/Professional/Ticket';
        });
        
        $('#btnCancel').click(function () {
            window.location.href = '/Professional/Ticket';
        });


        function validateEmail(email) {
            var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
            return emailReg.test(email);
        }


        function validatePhoneNo(phoneNO) {
            var emailReg = /\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/;
            return emailReg.test(phoneNO);
        }

        function validateInput()
        {
            if (!validateEmail($('#emailId').val())) {
                $('#emailId').focus();
                return false;
            }

            if (!validatePhoneNo($('#mobileNumber').val())) {
                $('#mobileNumber').focus();
                return false;
            }
            
            if($('#firstName').val()==='')
            {
                $('#firstName').focus();
                return false;
            }


            if ($('#lastName').val() === '') {
                $('#lastName').focus();
                return false;
            }
            if ($('#address').val() === '') {
                $('#address').focus();
                return false;
            }
            if ($('#city').val() === '') {
                $('#city').focus();
                return false;
            }
            if ($('#zipcode').val() === '') {
                $('#zipcode').focus();
                return false;
            }
           
            if ($('#mobileNumber').val() === '') {
                $('#mobileNumber').focus();
                return false;
            }
            if ($('#emailId').val() === '') {
                $('#emailId').focus();
                return false;
            }

            if ($('#fileName').val() === '') {
                $('#spnError').show();
                  return false;
            }
            //if ($('#fileSpareName').val() === '') { 
            //    $('#spnSpareError').show();
            //        return false;
            //}
            
          
            return true;
            

        }

    });

})();