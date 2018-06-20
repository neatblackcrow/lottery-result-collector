class RoundBox extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="panel panel-default">
                <div className="panel-heading">ข้อมูลงวด</div>
                <div className="panel-body">
                    <p>งวดที่: {this.props.round.round}</p>
                    <p>ประจำวันที่: {this.props.round.date}</p>
                </div>
            </div>
            );
    }
}