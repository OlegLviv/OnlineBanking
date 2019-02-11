import React from 'react';
import BaseContainer from '../BaseContainer';
import ChangePinForm from '../../components/ChangePinForm/ChangePinForm';

import { connect } from 'react-redux';
import { changePin } from '../../actions/crediCards';

class CreditCardEditPinContainer extends BaseContainer {
    onSendChangePin = body => this.props
        .changePin({
            newPin: body.newPin,
            id: this.props.match.params.id
        })
        .then(() => this.setState({ successMessage: "Pin changed" }))
        .catch(er => {
            if (er && er.status && er.status === 400)
                this.setState({ errorMessage: er.data });
        });

    render() {
        return (
            <ChangePinForm onSendChangePin={this.onSendChangePin} />
        );
    }
}

export default connect(
    state => ({

    }),
    dispatch => ({
        changePin: body => dispatch(changePin(body))
    })
)(CreditCardEditPinContainer);