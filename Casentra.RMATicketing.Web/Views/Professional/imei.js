(function () {

    $(function () {
      
        
        $('#btnUpload').click(function (e) {

            
            e.preventDefault();

            abp.ui.setBusy(
                $('#imeiForm'),
                abp.ajax({
                    url: abp.appPath + 'Professional/UploadIMEI',
                    type: 'POST',
                    data: JSON.stringify({
                        FileName: $('#fileName').val(),
                    })
                })
                .done(function (data) {
                    if (data > 0) {
                        
                        abp.notify.info('Saved Successfully');
                                           
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
            window.location.href = '/Professional/IMEI';
        });
        

      
     

    });

})();