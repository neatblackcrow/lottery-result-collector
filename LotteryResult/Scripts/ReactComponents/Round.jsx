class Round extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div class="panel panel-default">
                <div class="panel-heading">ข้อมูลงวด</div>
                <div class="panel-body">
                    <p>งวดที่: {this.props.round.round1}</p>
                    <p>ประจำวันที่: {this.props.round.date}</p>
                </div>
            </div>
            );
    }
}