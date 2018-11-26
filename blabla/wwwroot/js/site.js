// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
jQuery(()=>{


    jQuery('.delete-btn').on('click', (event)=>{
        var id = event.target.dataset.id;
        console.debug(id);
        jQuery.ajax({
            method: "DELETE",
            url: "api/employee/delete",
            data: { Id: id,},
            success: ()=> {window.location.reload()},
            error : ()=>{alert('error')}
        })
    })

    jQuery('.edit-btn').on('click', (event)=>{
        var id = event.target.dataset.id;
        console.debug(id);
        window.location.href = '/home/edit?id='+ id;
    })
})