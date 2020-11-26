var dataArray =[];
var id = null;
var editbtn = null;
function getEmployeeList(){
    $.get("http://localhost:5000/Employee").done(data=>{
    dataArray  = data.data;
    console.log(dataArray)
    let output="";    
    for(let record of dataArray){
            output+=`<tr>
                <td>${record.id}</td>
                <td>${record.name}</td>
                <td id="data__buttons">
                    <button class="edit-btn" value=${record.id}> <img src="./images/create-24px.svg" alt="Edit"></button>
                    <button class="delete-btn" value=${record.id}> <img src="./images/delete-24px.svg" alt="Delete"></button>
                </td>
                </tr>`;
            }
            $(".data__list").html(output);
            $("sub").text(`(${dataArray.length} employees)`);
        }
    ).fail(err=> console.log(err))
}

$(document).ready(function()
{
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
            url:"http://localhost:5000/employee/"+currentBtn.val(),
            method:"DELETE",
            success:function(){
                $(currentBtn).parent().parent().remove();
            },
            error:function(err){
                alert("Something went wrong! Please try again later")
            }
        })
    })

    $(document).on("click","#save-btn",function(e){
        e.preventDefault();
        let username = $('#username').val();
        let email = $('#email-id').val();
        let password  = $('#password').val();
        $.ajax({
            url:"http://localhost:5000/employee/"+id,
            method:"PUT",
            data:{
                name : username ? username : getFilteredData("name"),
                email : email ? email : getFilteredData("address"), 
                password : password ? password : getFilteredData("phoneNumber"), 
            },success: function(){
                $('#saved-img').append("<img src='./images/check_circle-24px.svg' alt='edit successful'/>");
                $('#save-btn').text("saved");
                editbtn.parent().prev().text(username);
            },error: err=>console.log(err)
        })
    })

    function getFilteredData(field){
        return dataArray.filter(emp => emp.id == id).filter(emp=> emp.hasOwnProperty(field)).map(emp=> emp[field]);
    }
});    