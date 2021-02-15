var createRoombtn = document.getElementById('create-room-btn')
var createRoomModal = document.getElementById('create-room-modal')

createRoombtn.addEventListener('click',function() {
    createRoomModal.classList.add('active')
})

function closeModal(){
    createRoomModal.classList.remove('active')
}