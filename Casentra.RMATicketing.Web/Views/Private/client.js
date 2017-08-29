(function () {

    $(function () {
      
         
        $('#firstName').focus();
        
        showIcloud();
        showPassword();
                
        $('.datepicker').datepicker({
            format: 'dd/mm/yyyy',            
        });

        $('#phonePassword').focus(function () {
            $(this).val('');
        });

        $('#iCloudAddress').focus(function () {
            $(this).val('');
        });

        $('#iCloudPassword').focus(function () {
            $(this).val('');
        });

        $('#btnTicket').click(function (e) {
            $('#spnError').html("");
            if (validateInput() != true)
                return false;

            if ($('#hidAccessories').val() === '') {
                $("#spnAccessories").html("please select atleast one");
                return false;
            }

            if ($('#hidProblems').val() === '') {
                $("#spnProblems").html("please select atleast one");
                return false;
            }

            e.preventDefault();
      
            abp.ui.setBusy(
                $('#ticketForm'),
                abp.ajax({
                    url: abp.appPath + 'Private/SaveTicket',
                    type: 'POST',
                    data: JSON.stringify({

                        FirstName: $('#firstName').val(),
                        LastName: $('#lastName').val(),
                        Address: $('#address').val(),
                        City: $('#city').val(),
                        Zipcode: $('#zipcode').val(),
                        
                        MobileNumber: $('#mobileNumber').val(),
                        Email: $('#emailId').val(),

                        purchasedDate: $('#purchasedDate').val(),
                        BoughtAtId: $('#BoughtAtId').val(),

                        ProductId: $('#ProductId').val(),
                        BrandId: $('#BrandId').val(),
                        ColorId: $('#ColorId').val(),
                        CapacityId: $('#CapacityId').val(),
                        IMEINumber: $('#iemiNumber').val(),
                        Password: $('#phonePassword').val(),
                        IcloudAddress: $('#iCloudAddress').val(),
                        IssueSummary: $('#issueSummary').val(),

                        IcloudPassword: $('#iCloudPassword').val(),
                        AccessoryId: $('#AccessoryId').val(),
                        PhoneConditionId: $('#PhoneConditionId').val(),
                        PhoneProblemId: $('#PhoneProblemId').val(),
                        Accessories: $('#hidAccessories').val(),
                        Problems: $('#hidProblems').val(),
                        ProblemIds: $('#hidProblemIds').val(),
                        IsProfessional: true,
                        IsTrading:false
                    })
                })
                .done(function (data) {
                    if (data > 0) {
                        
                        $('#confirmationModel').modal();
                        abp.notify.info('Saved Successfully');
                   
                    } else {
                        $('#spnError').html(data);
                        $('#errorModel').modal();
                       
                    }
                })
                
            );

            
        });

       

        $('#btnPrint').click(function (e) {
            printTicket();
        });

        function printTicket()
        {
            var printContents = document.getElementById('frmTicketForm').innerHTML;
            var originalContents = document.body.innerHTML;
            document.body.innerHTML = printContents;
            window.print();
            document.body.innerHTML = originalContents;

        }
              

        $('#chkiCloud').change(function () {
            if ($(this).is(":checked")) {

                $('#iCloudPassword').val("AUCUN");
                $('#iCloudAddress').val("AUCUN");

            }
            else
            {              
                $('#iCloudPassword').val("");
                $('#iCloudAddress').val("");
                $('#iCloudAddress').focus();
            }
                
        });

        function showIcloud()
        {
            $("#divICloudAddress").show();
            $("#divICloudPassword").show();
        }
        
        function hideIcloud() {
            $("#divICloudAddress").hide();
            $("#divICloudPassword").hide();
        }

        function showPassword() {
            $("#divPin").show();
            
        }

        function hidePassword() {
            $("#divPin").hide();
           
        }



        $('.chkproblem').click(function () {

           
            var getchkid = $(this).attr('id');
            var isChecked = $('#' + getchkid ).is(':checked');

            if ($('#' + getchkid).is(':checked') == true) {
                $('#td-prob' + $(this).val()).css("color", "white");
                $('#td-prob' + $(this).val()).css("background-color", "#27b1d8");
                //alert($(this).attr('data-text'));
                
                $('#PhoneProblemId').val($(this).val())

                var problems = $('#hidProblems').val();
                if (problems.indexOf($(this).attr('data-text')) != -1) {
                    return;
                }

                //hidProblemIds
                var problemIds = $('#hidProblemIds').val();
                problemIds = problemIds + $(this).val() + ",";
                $('#hidProblemIds').val(problemIds);

                //alert(problemIds);

                problems = problems + $(this).attr('data-text') + ",";
                $('#hidProblems').val(problems);

            }
            else {
                $('#td-prob' + $(this).val()).css("color", "black");
                $('#td-prob' + $(this).val()).css("background-color", "white");
            }
        });

        $('.chkaccessory').click(function () {
                    
            var getchkid = $(this).attr('id');
            var isChecked = $('#' + getchkid).is(':checked');

            if ($('#' + getchkid).is(':checked') == true) {
                $('#td' + $(this).val()).css("color", "white");
                $('#td' + $(this).val()).css("background-color", "#27b1d8");
                $('#AccessoryId').val($(this).val());

                var problems = $('#hidAccessories').val();
                if (problems.indexOf($(this).attr('data-text')) != -1) {
                    return;
                }

                problems = problems + $(this).attr('data-text') + ",";
                $('#hidAccessories').val(problems);
            }
            else {
                $('#td' + $(this).val()).css("color", "black");
                $('#td' + $(this).val()).css("background-color", "white");
            }

        });

        $('#btnConfirm').click(function () {
            window.location.href = '/private/client';
        });
        
        $('#btnCancel').click(function () {
            window.location.href = '/private/client';
        });

        function validateEmail(email) {
            var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
            return emailReg.test(email);
        }


        function validatePhoneNo(phoneNO) {
            var emailReg = /\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/;
            return emailReg.test(phoneNO);
        }

        $('#chkPinCode').change(function () {
            if ($(this).is(":checked")) {

                $('#phonePassword').val("AUCUN");

            }
            else {
                $('#phonePassword').val("");
                $('#phonePassword').focus();
            }

        });

        $('#chkProbSelect').change(function () {
            if ($(this).is(":checked")) {

                $('#hidProblems').val("---")
                $('#hidProblemIds').val("");

            }
            else {
                $('#hidProblems').val("");
                
            }

        });
        


        function validateInput()
        {
            if (!validateEmail($('#emailId').val())) {
                $('#emailId').focus();
                return false;
            }

            //if (!validatePhoneNo($('#phoneNumber').val())) {
            //    $('#phoneNumber').focus();
            //    return false;
            //}

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

            //if ($('#phoneNumber').val() === '') {
            //    $('#phoneNumber').focus();
            //    return false;
            //}

            if ($('#mobileNumber').val() === '') {
                $('#mobileNumber').focus();
                return false;
            }
            if ($('#emailId').val() === '') {
                $('#emailId').focus();
                return false;
            }

            if ($('#purchasedDate').val() === '') {
                $('#purchasedDate').focus();
                return false;
            }

            if ($('#iemiNumber').val() === '') {
                $('#iemiNumber').focus();
                return false;
            }
           
            
            if ($('#chkiCloud').is(":checked") == true) {                              

                $('#iCloudPassword').val("AUCUN");
                $('#iCloudAddress').val("AUCUN");
            }
            else {
              
                if ($('#iCloudAddress').val() === '') {
                    $('#iCloudAddress').focus();
                    return false;
                }
                if ($('#iCloudPassword').val() === '') {
                    $('#iCloudPassword').focus();
                    return false;
                }

            }

            if ($('#chkPinCode').is(":checked") == true) {
                $('#phonePassword').val("AUCUN");               
            }
            else {
                  if ($('#phonePassword').val() === '') {
                    $('#phonePassword').focus();
                    return false;
                }

            }
           
          
            return true;
            

        }

    });

})();