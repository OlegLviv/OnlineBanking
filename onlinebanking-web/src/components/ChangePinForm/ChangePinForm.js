import React from 'react';

import {
    Form, Input, Button
} from 'antd';

const onSend = (onSendChangePin, form, event) => {
    event.preventDefault();

    form
        .validateFields()
        .then(e => onSendChangePin(e));
}

const changePinForm = ({ onSendChangePin, form }) => (
    <Form style={{padding:'2rem 30%'}} onSubmit={e => onSend(onSendChangePin, form, e)}>
        <Form.Item>
            {form.getFieldDecorator('newPin', {
                rules: [{
                    required: true,
                    message: 'Please input correct new pin',
                    pattern: /^[0-9]{4}$/
                }]
            })(
                <Input type="number" placeholder="Input new pin" />
            )}
        </Form.Item>
        <Form.Item>
            <Button type="primary" htmlType="submit">
                Change
            </Button>
        </Form.Item>
    </Form>
);

export default Form.create()(changePinForm);