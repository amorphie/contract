
const { v4: uuid4 } = require("uuid");

function generateNewRecordId(input) {

    console.log(input);
    
    let uuid = uuid4();

    console.log("set newRecordId in generateNewRecordId to :", uuid )
    tc.setVar("newRecordId", uuid);
}

module.exports =[generateNewRecordId];