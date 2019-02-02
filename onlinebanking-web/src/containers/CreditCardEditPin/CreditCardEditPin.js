import React from 'react';
import BaseContainer from '../BaseContainer';
import ChangePinForm from '../../components/ChangePinForm/ChangePinForm';

class CreditCardEditPin extends BaseContainer {
    onSendChangePin = body =>{

    }
    
    render() {
        return(
            <ChangePinForm onSendChangePin={this.onSendChangePin}/>
        );
    }
}

export default CreditCardEditPin;