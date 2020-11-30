var token = localStorage.getItem("token");
var dataArray =[];
var id = null;
var editbtn = null;

function redirect(err){
    if(err.status >= 400 && err.status < 500)
        window.location = "/html/login.html"
        localStorage.clear();
}

function getEmployeeList(){
    $.ajax({
        url:"/Employee", 
    headers:{
        "Authorization":token
    },success: function(data){
        dataArray  = data.data;
        console.log(dataArray)
        let output="";    
        for(let record of dataArray){
                output+=`<tr>
                    <td>${record.id}</td>
                    <td>${record.name}</td>
                    <td id="data__buttons">
                        <button class="edit-btn" value=${record.id}> <img src="../images/create-24px.svg" alt="Edit"></button>
                        <button class="delete-btn" value=${record.id}> <img src="../images/delete-24px.svg" alt="Delete"></button>
                    </td>
                    </tr>`;
                }
                $(".data__list").html(output);
                $("sub").text(`(${dataArray.length} employees)`);
            },
            error: function(err){
                redirect(err)
            }
    })
}

$(document).ready(function()
{
    console.log(token);
    if (token == null)
        window.location = "/html/login.html";
    getEmployeeList();
    $(document).on("click",".edit-btn",function  (e){
        e.preventDefault();
        id  = $(this).val();
        editbtn = $(this);
        console.log("Clickes", dataArray);
        $(".modal").toggleClass("active");
        $(".main").css("background","rgba(0,0,0,0.1)");
        $(".container").css("background","rgba(255,255,255,0.7)");
        $(".nav").css("pointer-events","none");
        $('#username').val(getFilteredData("name"));
        $('#address').val(getFilteredData("address"));
        $('#pno').val(getFilteredData("phoneNumber"));
    })

    $(document).on("click","#close",function(e){
        e.preventDefault();
        $(".modal").removeClass("active");
        $(".container").css("background","rgba(255,255,255,1)");
        $(".main").css("background","gainsboro");
        $(".nav").css("pointer-events","auto"); 
        id = null;
    })

    $(document).on("click",".delete-btn",function(e){
        e.preventDefault();
        let currentBtn = $(this);
        $.ajax({
            url:"/Employee/"+currentBtn.val(),
            method:"DELETE",
            headers:{
                "Authorization":token
            },
            success:function(){
                $(currentBtn).parent().parent().remove();
            },
            error:function(err){
                redirect(err)
            }
        })
    })

    $(document).on("click","#save-btn",function(e){
        e.preventDefault();
        let username = $('#username').val();
        let email = $('#email-id').val();
        let address  = $('#address').val();
        let phoneNumber = $('#phoneNumber').val();
        $.ajax({
            url:"/Employee/"+id,
            method:"PUT",
            data:{
                Name : username,
                Email : email, 
                Address: address,
                phoneNumber 
            },headers:{
                "Authorization":token
            },success: function(){
                $('#saved-img').append("<img src='./images/check_circle-24px.svg' alt='edit successful'/>");
                $('#save-btn').text("saved");
                editbtn.parent().prev().text(username);
            },error: function(err){ 
                redirect(err)
            }
        })
    })

    function getFilteredData(field){
        return dataArray.filter(emp => emp.id == id).filter(emp=> emp.hasOwnProperty(field)).map(emp=> emp[field]);
    }
});    